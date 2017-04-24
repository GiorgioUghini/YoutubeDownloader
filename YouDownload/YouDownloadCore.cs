using MediaToolkit;
using MediaToolkit.Model;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VideoLibrary;
using System;
using System.Collections.Generic;
using YouTubePlaylistAPI;

namespace YouDownload
{
    public class YouDownloadCore
    {
        private ProgressBar progressBar1 { get; set; }
        private ProgressBar progressBar2 { get; set; }
        private Button btnDownload { get; set; }
        private int songNumber;
        private int convertedSong;

        public void DownloadMP3(string[] links, string destPath, ProgressBar[] pbr, Button btnDown)
        {
            songNumber = links.Length;
            progressBar1 = pbr[0];
            progressBar2 = pbr[1];
            btnDownload = btnDown;
            if (links.Length==0)
                throw new Exception("Il file txt è vuoto");
            if (!Directory.Exists(destPath))
                throw new DirectoryNotFoundException("Il path di destinazione non esiste");
            YouTube youtube = YouTube.Default;
            convertedSong = 0;
            btnDownload.Invoke((MethodInvoker)delegate () { btnDownload.Enabled=false; });
            progressBar2.Invoke((MethodInvoker)delegate () { progressBar2.Maximum = 100 * links.Length; });
            foreach (string link in links)
            {
                YouTubeVideo audio = youtube.GetAllVideos(link).Where(e => e.AudioFormat == AudioFormat.Aac && e.AdaptiveKind == AdaptiveKind.Audio).ToList().FirstOrDefault();
                string filename = Path.ChangeExtension(Path.Combine(destPath, Path.GetFileNameWithoutExtension(audio.FullName)), "mp3");
                MediaFile inputFile = new MediaFile { Filename = audio.GetUri()};
                MediaFile outputFile = new MediaFile { Filename = filename };
                using (Engine engine = new Engine())
                {

                    engine.GetMetadata(inputFile);
                    if (this.progressBar1 != null)
                    {
                        engine.ConvertProgressEvent += engine_ConvertProgressEvent;
                        engine.ConversionCompleteEvent += engine_ConversionCompleteEvent;
                    }
                    engine.Convert(inputFile, outputFile);
                }
            }
        }

        private void engine_ConversionCompleteEvent(object sender, ConversionCompleteEventArgs e)
        {
            progressBar1.Invoke((MethodInvoker)delegate () { progressBar1.Value = 100; });
        }

        private void engine_ConvertProgressEvent(object sender, ConvertProgressEventArgs e)
        {   
            if ((convertedSong * 100 + (int)((e.ProcessedDuration.TotalSeconds / e.TotalDuration.TotalSeconds) * 100)) < progressBar2.Value)
            {
                convertedSong++;
            }
            progressBar1.Invoke((MethodInvoker)delegate () 
            {
                progressBar1.Value = (int) ((e.ProcessedDuration.TotalSeconds / e.TotalDuration.TotalSeconds) * 100);
            });
            progressBar2.Invoke((MethodInvoker)delegate () { progressBar2.Value = (convertedSong * 100 + (int)((e.ProcessedDuration.TotalSeconds / e.TotalDuration.TotalSeconds) * 100)); });
        }
        
        public void downloadPlaylist(string playlistID, string destPath, ProgressBar[] pbr, Button btnDown)
        {
            YouTubeServiceClient ytc = new YouTubeServiceClient();
            string[] playlistURL = playlistID.Split(new string[] { "list=" }, StringSplitOptions.None);
            List<IYouTubeSong> songlist = new List<IYouTubeSong>();
            songlist = ytc.GetPlayListSongs("giorgio.gioba@gmail.com", playlistURL[1]);
            var listOfStrings = new List<string>();
            foreach (IYouTubeSong canzone in songlist)
            {
                listOfStrings.Add("https://www.youtube.com/watch?v=" + canzone.SongId);
            }
            string[] youtubesongs = listOfStrings.ToArray();
            DownloadMP3(youtubesongs, destPath, pbr, btnDown);
        }
    }
}
