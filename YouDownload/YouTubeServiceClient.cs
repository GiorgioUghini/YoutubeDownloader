using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using System.Diagnostics;


namespace YouTubePlaylistAPI
{
    public class YouTubeServiceClient
    {
        private static YouTubeServiceClient instance;

        public static YouTubeServiceClient Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new YouTubeServiceClient();
                }
                return instance;
            }
        }

        public List<IYouTubeSong> GetPlayListSongs(string playListId)
        {
            List<IYouTubeSong> playListSongs = new List<IYouTubeSong>();

            try
            {
                YouTubeServiceClient service = new YouTubeServiceClient();
                service.GetPlayListSongsInternalAsync(playListId, playListSongs).Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    //TODO: Add Logging
                }
            }

            return playListSongs;
        }

        private async Task GetPlayListSongsInternalAsync(string playListId, List<IYouTubeSong> playListSongs)
        {
            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = @"AIzaSyAVFJtmhysTe-0d7nYjbXr4uSj1kSbBlLI",
                ApplicationName = this.GetType().ToString()
            });

    var channelsListRequest = youtubeService.Channels.List("contentDetails");
            channelsListRequest.Mine = true;
            var nextPageToken = "";
            while (nextPageToken != null)
            {
                PlaylistItemsResource.ListRequest listRequest = youtubeService.PlaylistItems.List("contentDetails");
                listRequest.MaxResults = 50;
                listRequest.PlaylistId = playListId;
                listRequest.PageToken = nextPageToken;
                var response = await listRequest.ExecuteAsync();
                if (playListSongs == null)
                {
                    playListSongs = new List<IYouTubeSong>();
                }
                foreach (var playlistItem in response.Items)
                {
                    VideosResource.ListRequest videoR = youtubeService.Videos.List("snippet,contentDetails,status");
                    videoR.Id = playlistItem.ContentDetails.VideoId;
                    var responseV = await videoR.ExecuteAsync();
                    if (responseV.Items.Count > 0)
                    {
                        KeyValuePair<string, string> parsedSong = SongTitleParser.ParseTitle(responseV.Items[0].Snippet.Title);
                        ulong? duration = new DurationParser().GetDuration(responseV.Items[0].ContentDetails.Duration);
                        IYouTubeSong currentSong = new YouTubeSong(parsedSong.Key, parsedSong.Value, responseV.Items[0].Snippet.Title, responseV.Items[0].Id, playlistItem.Id, duration);
                        playListSongs.Add(currentSong);
                        Debug.WriteLine(currentSong.Title);
                    }
                }
                nextPageToken = response.NextPageToken;
            }
        }
    }
}