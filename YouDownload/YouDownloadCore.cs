using MediaToolkit;
using MediaToolkit.Model;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using VideoLibrary;
using System;
using System.Net;
using System.Web;
using System.Web.Script.Serialization;
using System.Collections.Generic;

namespace YouDownload
{
    public class YouDownloadCore
    {
        private ProgressBar progressBar1 { get; set; }
        private ProgressBar progressBar2 { get; set; }
        private Button btnDownload { get; set; }
        private int songNumber;
        private int convertedSong;

        public ReturnError DownloadMP3(string[] links, string destPath, ProgressBar[] pbr, Button btnDown)
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
            ReturnError results = new ReturnError();
            results.errorNumber = 0;
            results.errorLinks = new List<string>();
            foreach (string link in links)
            {
                try
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
                catch (NullReferenceException e)
                {
                    results.errorNumber++;
                    results.errorLinks.Add(link);
                }
            }
            return results;
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
        
        public ReturnError downloadPlaylist(string playlistID, string destPath, ProgressBar[] pbr, Button btnDown)
        {
            string[] playlistURLs = playlistID.Split(new string[] { "list=" }, StringSplitOptions.None);
            ReturnError errors = new ReturnError();
            btnDownload.Invoke((MethodInvoker)delegate () { btnDownload.Enabled = false; });
            YTResponse ytResponse = null;
            string url = @"https://www.googleapis.com/youtube/v3/playlistItems";
            var uriBuilder = new UriBuilder(url);
            var query = HttpUtility.ParseQueryString(uriBuilder.Query);
            query["part"] = "contentDetails";
            query["maxResults"] = "50";
            query["playlistId"] = playlistURLs[1];
            query["key"] = "AIzaSyCtzP23AG4P_K5WNIjqb7AOFnhWNyLIkoE";
            while (ytResponse == null || !string.IsNullOrWhiteSpace(ytResponse.nextPageToken))
            {
                if (ytResponse != null && !string.IsNullOrWhiteSpace(ytResponse.nextPageToken))
                    query["pageToken"] = ytResponse.nextPageToken;
                uriBuilder.Query = query.ToString();
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uriBuilder.ToString());
                request.AutomaticDecompression = DecompressionMethods.GZip;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    ytResponse = new JavaScriptSerializer().Deserialize<YTResponse>(reader.ReadToEnd());
                }
                string textResponse = string.Join(Environment.NewLine, ytResponse.items.Select(x => x.contentDetails.videoId).ToArray());
                string[] youtubesongs = textResponse.Split(new string[] { Environment.NewLine }, StringSplitOptions.None);
                for (int i=0;i<youtubesongs.Length;i++)
                {
                    youtubesongs[i] = "https://www.youtube.com/watch?v=" + youtubesongs[i];
                }
                errors = DownloadMP3(youtubesongs, destPath, pbr, btnDown);
            }
            return errors;
        }
    }
}
