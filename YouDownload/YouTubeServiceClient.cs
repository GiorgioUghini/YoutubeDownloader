﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Google.Apis.Auth.OAuth2;
using Google.Apis.Services;
using Google.Apis.Util.Store;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
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

        public List<IYouTubeSong> GetPlayListSongs(string userEmail, string playListId)
        {
            List<IYouTubeSong> playListSongs = new List<IYouTubeSong>();

            try
            {
                YouTubeServiceClient service = new YouTubeServiceClient();
                service.GetPlayListSongsInternalAsync(userEmail, playListId, playListSongs).Wait();
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

        public bool RemoveSongFromPlaylist(string userEmail, string playlistItemId)
        {
            bool isSuccessfullyRemoved = false;

            try
            {
                YouTubeServiceClient service = new YouTubeServiceClient();
                service.RemoveSongFromPlaylistAsync(userEmail, playlistItemId).Wait();
                isSuccessfullyRemoved = true;
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    //TODO: Add Logging
                    isSuccessfullyRemoved = false;
                }
            }

            return isSuccessfullyRemoved;
        }

        private async Task RemoveSongFromPlaylistAsync(string userEmail, string playlistItemId)
        {
            var youtubeService = await this.GetYouTubeService(userEmail);
            PlaylistItemsResource.DeleteRequest deleteRequest = youtubeService.PlaylistItems.Delete(playlistItemId);
            string result = await deleteRequest.ExecuteAsync();
        }

        public bool AddSongToPlaylist(string userEmail, string songId, string playlistId)
        {
            bool isSuccessfullyAdded = false;

            try
            {
                YouTubeServiceClient service = new YouTubeServiceClient();
                service.AddSongToPlaylistAsync(userEmail, songId, playlistId).Wait();
                isSuccessfullyAdded = true;
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    //TODO: Add Logging
                    isSuccessfullyAdded = false;
                }
            }

            return isSuccessfullyAdded;
        }

        public bool UpdateSongPositionInPlaylist(
            string userEmail,
            string playlistId,
            YouTubeSong song,
            int position)
        {
            bool isSuccessfullyUpdated = false;

            try
            {
                YouTubeServiceClient service = new YouTubeServiceClient();
                service.UpdatePlaylistItemAsync(userEmail, song.SongId, playlistId, song.PlayListItemId, position).Wait();
                isSuccessfullyUpdated = true;
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    //TODO: Add Logging
                    isSuccessfullyUpdated = false;
                }
            }

            return isSuccessfullyUpdated;
        }

        public List<YouTubePlayList> GetUserPlayLists(string userEmail)
        {
            List<YouTubePlayList> playLists = new List<YouTubePlayList>();

            try
            {
                YouTubeServiceClient service = new YouTubeServiceClient();
                service.GetUserPlayListsAsync(userEmail, playLists).Wait();
            }
            catch (AggregateException ex)
            {
                foreach (var e in ex.InnerExceptions)
                {
                    //TODO: Add Logging
                }
            }

            return playLists;
        }

        private async Task GetPlayListSongsInternalAsync(string userEmail, string playListId, List<IYouTubeSong> playListSongs)
        {
            var youtubeService = await this.GetYouTubeService(userEmail);

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

        private async Task GetUserPlayListsAsync(string userEmail, List<YouTubePlayList> playLists)
        {
            var youtubeService = await this.GetYouTubeService(userEmail);

            var channelsListRequest = youtubeService.Channels.List("contentDetails");
            channelsListRequest.Mine = true;
            var playlists = youtubeService.Playlists.List("snippet");
            playlists.PageToken = "";
            playlists.MaxResults = 50;
            playlists.Mine = true;
            PlaylistListResponse presponse = await playlists.ExecuteAsync();
            foreach (var currentPlayList in presponse.Items)
            {
                playLists.Add(new YouTubePlayList(currentPlayList.Snippet.Title, currentPlayList.Id));
            }
        }

        private async Task AddSongToPlaylistAsync(string userEmail, string songId, string playlistId)
        {
            var youtubeService = await this.GetYouTubeService(userEmail);
            var newPlaylistItem = new PlaylistItem();
            newPlaylistItem.Snippet = new PlaylistItemSnippet();
            newPlaylistItem.Snippet.PlaylistId = playlistId;
            newPlaylistItem.Snippet.ResourceId = new ResourceId();
            newPlaylistItem.Snippet.ResourceId.Kind = "youtube#video";
            newPlaylistItem.Snippet.ResourceId.VideoId = songId;
            newPlaylistItem = await youtubeService.PlaylistItems.Insert(newPlaylistItem, "snippet").ExecuteAsync();
        }

        private async Task UpdatePlaylistItemAsync(string userEmail, string songId, string playlistId, string playlistItemId, int position)
        {
            var youtubeService = await this.GetYouTubeService(userEmail);
            var newPlaylistItem = new PlaylistItem();
            newPlaylistItem.Snippet = new PlaylistItemSnippet();
            newPlaylistItem.Snippet.PlaylistId = playlistId;
            newPlaylistItem.Snippet.ResourceId = new ResourceId();
            newPlaylistItem.Snippet.ResourceId.Kind = "youtube#video";
            newPlaylistItem.Snippet.ResourceId.VideoId = songId;
            newPlaylistItem.Snippet.Position = position;
            newPlaylistItem.Id = playlistItemId;
            newPlaylistItem = await youtubeService.PlaylistItems.Update(newPlaylistItem, "snippet,contentDetails,status").ExecuteAsync();
        }

        private async Task<YouTubeService> GetYouTubeService(string userEmail)
        {
            UserCredential credential;
            using (var stream = new FileStream(@"secret.json", FileMode.Open, FileAccess.Read))
            { 
                credential = await GoogleWebAuthorizationBroker.AuthorizeAsync(
                    GoogleClientSecrets.Load(stream).Secrets,
                    new[]
                    {
                        YouTubeService.Scope.Youtube,
                        YouTubeService.Scope.Youtubepartner,
                        YouTubeService.Scope.YoutubeUpload,
                        YouTubeService.Scope.YoutubepartnerChannelAudit,
                        YouTubeService.Scope.YoutubeReadonly
                    },
                    userEmail,
                    CancellationToken.None,
                    new FileDataStore(this.GetType().ToString()));
            }

            var youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                HttpClientInitializer = credential,
                ApplicationName = this.GetType().ToString()
            });

            return youtubeService;
        }
    }
}