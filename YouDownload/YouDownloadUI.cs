using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace YouDownload
{
    public partial class YouDownloadUI : Form
    {
        private static readonly YouDownloadCore youDownload = new YouDownloadCore();
        private BackgroundWorker bkgWorker = null;

        private void customInitialize(){
            bkgWorker = new BackgroundWorker();
            bkgWorker.DoWork += new DoWorkEventHandler(bkgWorker_DoWork);
            bkgWorker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bkgWorker_RunWorkerCompleted);
            bkgWorker.ProgressChanged += new ProgressChangedEventHandler(bkgWorker_ProgressChanged);

            //Defaults Values
            txtPath.Text = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
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
            MessageBox.Show("Operazione terminata", "Informazione", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void bkgWorker_DoWork(object sender, DoWorkEventArgs e)
        {
            try
            {
                resetPbr();
                if (checkPlaylistvideo.Checked)
                {
                    string[] elements = System.IO.File.ReadAllLines(textBox1.Text);
                    youDownload.DonwloadMP3(elements, txtPath.Text, pbrConvert, pbrTotal);
                }
                else
                {
                    string[] elements = { txtUrl.Text };
                    youDownload.DonwloadMP3(elements, txtPath.Text, pbrConvert, pbrTotal);
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