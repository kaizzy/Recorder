namespace Recorder5
{
    partial class Form1
    {
        /// <summary>
        /// 必要なデザイナー変数です。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 使用中のリソースをすべてクリーンアップします。
        /// </summary>
        /// <param name="disposing">マネージド リソースを破棄する場合は true を指定し、その他の場合は false を指定します。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows フォーム デザイナーで生成されたコード

        /// <summary>
        /// デザイナー サポートに必要なメソッドです。このメソッドの内容を
        /// コード エディターで変更しないでください。
        /// </summary>
        private void InitializeComponent()
        {
            this.button_refresh = new System.Windows.Forms.Button();
            this.listview_sources = new System.Windows.Forms.ListView();
            this.button_Start = new System.Windows.Forms.Button();
            this.button_stop = new System.Windows.Forms.Button();
            this.btn_Upload = new System.Windows.Forms.Button();
            this.btn_toText = new System.Windows.Forms.Button();
            this.btn_translat = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // button_refresh
            // 
            this.button_refresh.Location = new System.Drawing.Point(292, 43);
            this.button_refresh.Name = "button_refresh";
            this.button_refresh.Size = new System.Drawing.Size(237, 23);
            this.button_refresh.TabIndex = 0;
            this.button_refresh.Text = "Reflesh_Source";
            this.button_refresh.UseVisualStyleBackColor = true;
            this.button_refresh.Click += new System.EventHandler(this.button_refresh_Click);
            // 
            // listview_sources
            // 
            this.listview_sources.Location = new System.Drawing.Point(31, 43);
            this.listview_sources.Name = "listview_sources";
            this.listview_sources.Size = new System.Drawing.Size(219, 96);
            this.listview_sources.TabIndex = 1;
            this.listview_sources.UseCompatibleStateImageBehavior = false;
            // 
            // button_Start
            // 
            this.button_Start.Location = new System.Drawing.Point(292, 87);
            this.button_Start.Name = "button_Start";
            this.button_Start.Size = new System.Drawing.Size(99, 23);
            this.button_Start.TabIndex = 2;
            this.button_Start.Text = "Rec Start";
            this.button_Start.UseVisualStyleBackColor = true;
            this.button_Start.Click += new System.EventHandler(this.button_Start_Click);
            // 
            // button_stop
            // 
            this.button_stop.Location = new System.Drawing.Point(425, 87);
            this.button_stop.Name = "button_stop";
            this.button_stop.Size = new System.Drawing.Size(111, 23);
            this.button_stop.TabIndex = 3;
            this.button_stop.Text = "Rec Stop";
            this.button_stop.UseVisualStyleBackColor = true;
            this.button_stop.Click += new System.EventHandler(this.button_stop_Click);
            // 
            // btn_Upload
            // 
            this.btn_Upload.Location = new System.Drawing.Point(292, 129);
            this.btn_Upload.Name = "btn_Upload";
            this.btn_Upload.Size = new System.Drawing.Size(244, 23);
            this.btn_Upload.TabIndex = 4;
            this.btn_Upload.Text = "Google Storageへのアップロード";
            this.btn_Upload.UseVisualStyleBackColor = true;
            this.btn_Upload.Click += new System.EventHandler(this.btn_Upload_Click);
            // 
            // btn_toText
            // 
            this.btn_toText.Location = new System.Drawing.Point(292, 168);
            this.btn_toText.Name = "btn_toText";
            this.btn_toText.Size = new System.Drawing.Size(244, 23);
            this.btn_toText.TabIndex = 5;
            this.btn_toText.Text = "テキスト変換";
            this.btn_toText.UseVisualStyleBackColor = true;
            this.btn_toText.Click += new System.EventHandler(this.btn_toText_Click);
            // 
            // btn_translat
            // 
            this.btn_translat.Location = new System.Drawing.Point(292, 206);
            this.btn_translat.Name = "btn_translat";
            this.btn_translat.Size = new System.Drawing.Size(244, 23);
            this.btn_translat.TabIndex = 6;
            this.btn_translat.Text = "英語変換";
            this.btn_translat.UseVisualStyleBackColor = true;
            this.btn_translat.Click += new System.EventHandler(this.btn_translat_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btn_translat);
            this.Controls.Add(this.btn_toText);
            this.Controls.Add(this.btn_Upload);
            this.Controls.Add(this.button_stop);
            this.Controls.Add(this.button_Start);
            this.Controls.Add(this.listview_sources);
            this.Controls.Add(this.button_refresh);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_refresh;
        private System.Windows.Forms.ListView listview_sources;
        private System.Windows.Forms.Button button_Start;
        private System.Windows.Forms.Button button_stop;
        private System.Windows.Forms.Button btn_Upload;
        private System.Windows.Forms.Button btn_toText;
        private System.Windows.Forms.Button btn_translat;
    }
}

