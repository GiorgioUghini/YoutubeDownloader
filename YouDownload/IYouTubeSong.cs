using System;

namespace YouTubePlaylistAPI
    {
        public interface IYouTubeSong
        {
            string Artist { get; set; }

            string SongId { get; set; }

            string Title { get; set; }

            string PlayListItemId { get; set; }

            ulong? Duration { get; set; }

            string OriginalTitle { get; set; }

            Guid SongGuid { get; set; }
        }
    }
