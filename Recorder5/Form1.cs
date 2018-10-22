using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using NAudio.Wave;
using System.IO;
using Google.Cloud.Translation.V2;
using Google.Cloud.Speech.V1;
using Google.Cloud.Storage.V1;


namespace Recorder5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button_refresh_Click(object sender, EventArgs e)
        {
                        // setup WaveIn Devices
            //souce をクラス化：WaveInCapabilities
            List<WaveInCapabilities> sources = new List<WaveInCapabilities>();
            
            //sources に 利用できるデバイスをDeviceCount 数分 追加する。
            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                sources.Add(WaveIn.GetCapabilities(i));
            }
            
            listview_sources.Items.Clear();
            
            //listview_sourc にitem を表示する。

            foreach (var source in sources)
            {
                ListViewItem item = new ListViewItem(source.ProductName);
                item.SubItems.Add(new ListViewItem.ListViewSubItem(item, source.Channels.ToString()));

                listview_sources.Items.Add(item);
            }
        }


        #region Member

        private NAudio.Wave.WaveIn sourceStream = null;
        private NAudio.Wave.DirectSoundOut waveOut = null;
        private NAudio.Wave.WaveFileWriter waveWriter = null;




        #endregion

        private void button_Start_Click(object sender, EventArgs e)
        {

            //マイク元を指定していない場合。
            if (listview_sources.SelectedItems.Count == 0) return;

            //オーディオチェーン:WaveIn(rec)  ⇒ Callback() ⇒ waveWriter

            //録音先のwavファイル
            SaveFileDialog save = new SaveFileDialog();
            save.Filter = "Wave File (*.wav)|*.wav;";
            if (save.ShowDialog() != DialogResult.OK) return;

            //選択した録音デバイス番号
            int deviceNumber = listview_sources.SelectedItems[0].Index;

            //waveIn selet Recording Deivce

            sourceStream = new WaveIn();　　//sourceStreamは、78で定義
            sourceStream.DeviceNumber = deviceNumber;
            //  sourceStream.WaveFormat = new WaveFormat(16000, WaveIn.GetCapabilities(deviceNumber).Channels);
            sourceStream.WaveFormat = new WaveFormat(16000, 1);

            //録音のコールバックkな数 k??
            sourceStream.DataAvailable += new EventHandler<WaveInEventArgs>(sourceStream_DataAvailable);

            //wave 出力
            waveWriter = new WaveFileWriter(save.FileName, sourceStream.WaveFormat);


            //Label
            this.Label_Status.Text = "録音中" + "\r\n" + "開始時間:" + DateTime.Now; ;

            //録音開始
            sourceStream.StartRecording();

        }

        private void sourceStream_DataAvailable(object sender, WaveInEventArgs e)
        {
            if (waveWriter == null) return;

            waveWriter.Write(e.Buffer, 0, e.BytesRecorded);
            waveWriter.Flush();
        }

        private void button_stop_Click(object sender, EventArgs e)
        {
            waveOut?.Stop();
            waveOut?.Dispose();
            waveOut = null;

            sourceStream?.StopRecording();
            sourceStream?.Dispose();
            sourceStream = null;

            waveWriter?.Dispose();
            waveWriter = null;

            //Label
            this.Label_Status.Text = "待機中";



        }

        private void btn_Upload_Click(object sender, EventArgs e)
        {

            //https://qiita.com/shiki_hskw/items/b8f365d6b8075bf4c29b
            //https://leadtools.grapecity.com/topics/news-20170726
            //https://cloud.google.com/storage/docs/object-basics?hl=ja#storage-download-object-python

            string bucketName = "kh1009";
            string localPath = @"D:\Temp\";
            string fileName = "translate.wav";

            //label
            this.Label_Status.Text = "Google Cloud ストレージ へアップ中" + "\r\n" + "開始時間:" + DateTime.Now;

            try
            {
                var storage = StorageClient.Create();
                using (var f = File.OpenRead(localPath + "/" + fileName))
                {
                    //objectName = objectName ?? Path.GetFileName(localPath);
                    storage.UploadObject(bucketName, fileName, null, f);
                    MessageBox.Show($"Uploaded {fileName}.");
                }
            }
            catch (Exception exc)
            {
                MessageBox.Show(exc.Message);
                //Console.WriteLine(exc.Message);
                return;
            }

            //label
            this.Label_Status.Text = "待機中";
        }

        static object AsyncRecognize(string storageUri)
        {
            var speech = SpeechClient.Create();
            DateTime Start_time = DateTime.Now;

            var longOperation = speech.LongRunningRecognize(
                new RecognitionConfig()
                {
                    Encoding = RecognitionConfig.Types.AudioEncoding.Linear16,
                    SampleRateHertz = 16000,
                    LanguageCode = "ja-JP",
                },
                RecognitionAudio.FromStorageUri(storageUri));

            longOperation = longOperation.PollUntilCompleted();
            var response = longOperation.Result;

            foreach (var result in response.Results)
            {
                foreach (var alternatime in result.Alternatives)
                {
                    File.AppendAllText(@"D:\temp\transcript.txt", alternatime.Transcript + Environment.NewLine);                    
                }
            }

            DateTime End_time = DateTime.Now;
            MessageBox.Show("利用時間: " +  Convert.ToString(End_time-Start_time));
            return 0;
        }
        private void btn_toText_Click(object sender, EventArgs e)
        {
            //The path should be gs://<bucket_name>/<file_path_inside_bucket>.  
            string storageUri = "gs://kh1009/translate.wav";

            //Console.WriteLine(storageUri);
            //File_Name.Text = "storage URI: " + storageUri;

            //System.Threading.Thread.Sleep(50);

            //label
            this.Label_Status.Text = "テキスト変換中" + "\r\n" + "開始時間:" + DateTime.Now;


            AsyncRecognize(storageUri);

            //label
            this.Label_Status.Text = "待機中";
            //Console.ReadKey();


        }

        private void btn_translat_Click(object sender, EventArgs e)
        {
            string s;
            string text = "";
            FileStream fin;
            DateTime Start_time = DateTime.Now;

            //label

            this.Label_Status.Text = "英語変換中" + "\r\n" + "開始時間:" + DateTime.Now;

            fin = new FileStream(@"D:\Temp\transcript.txt", FileMode.Open);
            StreamReader fstr_in = new StreamReader(fin);

            while ((s = fstr_in.ReadLine()) != null)
            {
                text = text + s;
            }
            //string text = @"D:\Temp\transcript.txt";
            fstr_in.Close();


            string targetLanguageCode = "en";
            string sourceLanguageCode = "ja";


            TranslationClient client = TranslationClient.Create();
            var response = client.TranslateText(text, targetLanguageCode, sourceLanguageCode);
            //string response = client.TranslateText(text, targetLanguageCode, sourceLanguageCode);

            File.AppendAllText(@"D:\temp\English_txt.txt", Convert.ToString(response.TranslatedText));
            
            DateTime End_time = DateTime.Now;            
            MessageBox.Show("利用時間: " + Convert.ToString(End_time-Start_time));

            this.Label_Status.Text = "待機中";
            //Console.WriteLine(resonse.TranslatedText);
            //Console.ReadKey();

        }
    }
}
