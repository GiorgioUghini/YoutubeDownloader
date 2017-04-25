using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace YouDownload
{
    namespace ProgressBarSample
    {

        public enum ProgressBarDisplayText
        {
            Percentage,
            CustomText
        }

        class CustomProgressBar : ProgressBar
        {
            //Property to set to decide whether to print a % or Text
            private ProgressBarDisplayText m_DisplayStyle;
            public ProgressBarDisplayText DisplayStyle
            {
                get { return m_DisplayStyle; }
                set { m_DisplayStyle = value; }
            }

            //Property to hold the custom text
            private string m_CustomText;
            public string CustomText
            {
                get { return m_CustomText; }
                set
                {
                    m_CustomText = value;
                    this.Invalidate();
                }
            }

            private const int WM_PAINT = 0x000F;
            protected override void WndProc(ref Message m)
            {
                base.WndProc(ref m);

                switch (m.Msg)
                {
                    case WM_PAINT:
                        int m_Percent = Convert.ToInt32((Convert.ToDouble(Value) / Convert.ToDouble(Maximum)) * 100);
                        dynamic flags = TextFormatFlags.VerticalCenter | TextFormatFlags.HorizontalCenter | TextFormatFlags.SingleLine | TextFormatFlags.WordEllipsis;

                        using (Graphics g = Graphics.FromHwnd(Handle))
                        {
                            using (Brush textBrush = new SolidBrush(ForeColor))
                            {

                                switch (DisplayStyle)
                                {
                                    case ProgressBarDisplayText.CustomText:
                                        TextRenderer.DrawText(g, CustomText, new Font("Arial", Convert.ToSingle(8.25), FontStyle.Regular), new Rectangle(0, 0, this.Width, this.Height), Color.Black, flags);
                                        break;
                                    case ProgressBarDisplayText.Percentage:
                                        TextRenderer.DrawText(g, string.Format("{0}%", m_Percent), new Font("Arial", Convert.ToSingle(9.25), FontStyle.Bold), new Rectangle(0, 0, this.Width, this.Height), Color.Black, flags);
                                        break;
                                }

                            }
                        }

                        break;
                }

            }
        }
    }

    public partial class YouDownloadUI : Form
    {
        public ReturnError errori = new ReturnError();
        private static readonly YouDownloadCore youDownload = new YouDownloadCore();
        private BackgroundWorker bkgWorker = null;

        private void customInitialize(){
            bkgWorker = new BackgroundWorker();
            bkgWorker.DoWork += new DoWorkEventHandler(bkgWorker_DoWork);
            bkgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkgWorker_RunWorkerCompleted);
            bkgWorker.ProgressChanged += new ProgressChangedEventHandler(bkgWorker_ProgressChanged);

            //Defaults Values
            txtPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            Text = "Youtube MP3 Downloader";
        }

        public YouDownloadUI()
        {
            InitializeComponent();
            customInitialize();
        }


        private void bkgWorker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            MessageBox.Show("Test", "Informazione", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bkgWorker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            pbrTotal.Maximum = 100;
            pbrTotal.Value = 100;
            if (errori.errorNumber!=0)
            {
                MessageBox.Show(String.Concat("Canzoni non scaricate: ",errori.errorNumber.ToString(),"\n\nE' possibile trovare la lista di canzoni non scaricate nella cartella della musica"), "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
                foreach (string link in errori.errorLinks)
                {
                    System.IO.File.AppendAllText(String.Concat(txtPath.Text,@"\Errori.txt"), link+"\n");
                }
            }
            MessageBox.Show("Operazione terminata", "Informazione", MessageBoxButtons.OK, MessageBoxIcon.Information);
            btnDownload.Enabled = true;
        }

        private void bkgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                resetPbr();
                ProgressBar[] pbr = { pbrConvert, pbrTotal };
                if (checkPlaylistvideo.Checked)
                {
                    if (textBox1.Text.EndsWith(".txt"))
                    {
                        string[] elements = System.IO.File.ReadAllLines(textBox1.Text);
                        errori = youDownload.DownloadMP3(elements, txtPath.Text, pbr, btnDownload);
                    }
                    else
                    {
                        errori = youDownload.downloadPlaylist(textBox1.Text, txtPath.Text, pbr, btnDownload);
                    }
                    
                }
                else
                {
                    string[] elements = { txtUrl.Text };
                    errori = youDownload.DownloadMP3(elements, txtPath.Text, pbr, btnDownload);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Errore", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            
        }

        private void btnBrowse_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fbd = new FolderBrowserDialog();
            if (fbd.ShowDialog() == DialogResult.OK)
            {
                txtPath.Text = fbd.SelectedPath;
            }
        }

        private void btnDownload_Click(object sender, EventArgs e)
        {
            bkgWorker.RunWorkerAsync();   
        }

        private void btnAbout_Click(object sender, EventArgs e)
        {
            MessageBox.Show("A Federica", "Dedica", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void btnBrowseFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            fd.Filter = "File di testo (*.txt)|*.txt";
            if (fd.ShowDialog() == DialogResult.OK)
            {
                textBox1.Text = fd.FileName;
            }
        }

        private void checkSinglevideo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPlaylistvideo.Checked && checkSinglevideo.Checked)
            {
                checkPlaylistvideo.Checked = false;
            }
        }

        private void checkPlaylistvideo_CheckedChanged(object sender, EventArgs e)
        {
            if (checkPlaylistvideo.Checked && checkSinglevideo.Checked)
            {
                checkSinglevideo.Checked = false;
            }
        }

        private void resetPbr()
        {
            pbrConvert.Value = 0;
            pbrTotal.Value = 0;
        }
    }
}