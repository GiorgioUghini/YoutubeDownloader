namespace YouDownload
{
    partial class YouDownloadUI
    {
        /// <summary>
        /// Variabile di progettazione necessaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Pulire le risorse in uso.
        /// </summary>
        /// <param name="disposing">ha valore true se le risorse gestite devono essere eliminate, false in caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Codice generato da Progettazione Windows Form

        /// <summary>
        /// Metodo necessario per il supporto della finestra di progettazione. Non modificare
        /// il contenuto del metodo con l'editor di codice.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDownload = new System.Windows.Forms.Button();
            this.txtUrl = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtPath = new System.Windows.Forms.TextBox();
            this.btnBrowse = new System.Windows.Forms.Button();
            this.pbrConvert = new System.Windows.Forms.ProgressBar();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.btnBrowseFile = new System.Windows.Forms.Button();
            this.btnAbout = new System.Windows.Forms.Button();
            this.checkSinglevideo = new System.Windows.Forms.CheckBox();
            this.checkPlaylistvideo = new System.Windows.Forms.CheckBox();
            this.pbrTotal = new System.Windows.Forms.ProgressBar();
            this.label5 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // btnDownload
            // 
            this.btnDownload.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnDownload.Location = new System.Drawing.Point(218, 136);
            this.btnDownload.Name = "btnDownload";
            this.btnDownload.Size = new System.Drawing.Size(304, 40);
            this.btnDownload.TabIndex = 0;
            this.btnDownload.Text = "Download";
            this.btnDownload.UseVisualStyleBackColor = true;
            this.btnDownload.Click += new System.EventHandler(this.btnDownload_Click);
            // 
            // txtUrl
            // 
            this.txtUrl.Location = new System.Drawing.Point(115, 62);
            this.txtUrl.Name = "txtUrl";
            this.txtUrl.Size = new System.Drawing.Size(496, 20);
            this.txtUrl.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(21, 65);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(88, 13);
            this.label1.TabIndex = 2;
            this.label1.Text = "Url singolo video:";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(58, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(51, 13);
            this.label2.TabIndex = 3;
            this.label2.Text = "Save To:";
            // 
            // txtPath
            // 
            this.txtPath.Location = new System.Drawing.Point(115, 24);
            this.txtPath.Name = "txtPath";
            this.txtPath.Size = new System.Drawing.Size(496, 20);
            this.txtPath.TabIndex = 4;
            // 
            // btnBrowse
            // 
            this.btnBrowse.Location = new System.Drawing.Point(617, 22);
            this.btnBrowse.Name = "btnBrowse";
            this.btnBrowse.Size = new System.Drawing.Size(75, 23);
            this.btnBrowse.TabIndex = 5;
            this.btnBrowse.Text = "Sfoglia";
            this.btnBrowse.UseVisualStyleBackColor = true;
            this.btnBrowse.Click += new System.EventHandler(this.btnBrowse_Click);
            // 
            // pbrConvert
            // 
            this.pbrConvert.Location = new System.Drawing.Point(115, 192);
            this.pbrConvert.Name = "pbrConvert";
            this.pbrConvert.Size = new System.Drawing.Size(577, 23);
            this.pbrConvert.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(16, 197);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Progresso singolo:";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(115, 101);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(496, 20);
            this.textBox1.TabIndex = 9;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(50, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(59, 13);
            this.label4.TabIndex = 10;
            this.label4.Text = "Playlist .txt:";
            // 
            // btnBrowseFile
            // 
            this.btnBrowseFile.Location = new System.Drawing.Point(617, 98);
            this.btnBrowseFile.Name = "btnBrowseFile";
            this.btnBrowseFile.Size = new System.Drawing.Size(75, 23);
            this.btnBrowseFile.TabIndex = 11;
            this.btnBrowseFile.Text = "Sfoglia";
            this.btnBrowseFile.UseVisualStyleBackColor = true;
            this.btnBrowseFile.Click += new System.EventHandler(this.btnBrowseFile_Click);
            // 
            // btnAbout
            // 
            this.btnAbout.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F);
            this.btnAbout.Location = new System.Drawing.Point(115, 136);
            this.btnAbout.Name = "btnAbout";
            this.btnAbout.Size = new System.Drawing.Size(97, 40);
            this.btnAbout.TabIndex = 12;
            this.btnAbout.Text = "About";
            this.btnAbout.UseVisualStyleBackColor = true;
            this.btnAbout.Click += new System.EventHandler(this.btnAbout_Click);
            // 
            // checkSinglevideo
            // 
            this.checkSinglevideo.AutoSize = true;
            this.checkSinglevideo.Checked = true;
            this.checkSinglevideo.CheckState = System.Windows.Forms.CheckState.Checked;
            this.checkSinglevideo.Location = new System.Drawing.Point(543, 136);
            this.checkSinglevideo.Name = "checkSinglevideo";
            this.checkSinglevideo.Size = new System.Drawing.Size(127, 17);
            this.checkSinglevideo.TabIndex = 13;
            this.checkSinglevideo.Text = "Scarica singolo video";
            this.checkSinglevideo.UseVisualStyleBackColor = true;
            this.checkSinglevideo.CheckedChanged += new System.EventHandler(this.checkSinglevideo_CheckedChanged);
            // 
            // checkPlaylistvideo
            // 
            this.checkPlaylistvideo.AutoSize = true;
            this.checkPlaylistvideo.Location = new System.Drawing.Point(543, 159);
            this.checkPlaylistvideo.Name = "checkPlaylistvideo";
            this.checkPlaylistvideo.Size = new System.Drawing.Size(113, 17);
            this.checkPlaylistvideo.TabIndex = 14;
            this.checkPlaylistvideo.Text = "Scarica playlist .txt";
            this.checkPlaylistvideo.UseVisualStyleBackColor = true;
            this.checkPlaylistvideo.CheckedChanged += new System.EventHandler(this.checkPlaylistvideo_CheckedChanged);
            // 
            // pbrTotal
            // 
            this.pbrTotal.Location = new System.Drawing.Point(115, 226);
            this.pbrTotal.Name = "pbrTotal";
            this.pbrTotal.Size = new System.Drawing.Size(577, 23);
            this.pbrTotal.Step = 1;
            this.pbrTotal.TabIndex = 15;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(23, 231);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(86, 13);
            this.label5.TabIndex = 16;
            this.label5.Text = "Progresso totale:";
            // 
            // YouDownloadUI
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(743, 262);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pbrTotal);
            this.Controls.Add(this.checkPlaylistvideo);
            this.Controls.Add(this.checkSinglevideo);
            this.Controls.Add(this.btnAbout);
            this.Controls.Add(this.btnBrowseFile);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.pbrConvert);
            this.Controls.Add(this.btnBrowse);
            this.Controls.Add(this.txtPath);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtUrl);
            this.Controls.Add(this.btnDownload);
            this.Name = "YouDownloadUI";
            this.Text = "YouDownload";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnDownload;
        private System.Windows.Forms.TextBox txtUrl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtPath;
        private System.Windows.Forms.Button btnBrowse;
        private System.Windows.Forms.ProgressBar pbrConvert;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnBrowseFile;
        private System.Windows.Forms.Button btnAbout;
        private System.Windows.Forms.CheckBox checkSinglevideo;
        private System.Windows.Forms.CheckBox checkPlaylistvideo;
        private System.Windows.Forms.ProgressBar pbrTotal;
        private System.Windows.Forms.Label label5;
    }
}

