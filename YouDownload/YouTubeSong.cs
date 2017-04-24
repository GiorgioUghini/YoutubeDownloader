using System;

namespace YouTubePlaylistAPI
{
    public class YouTubeSong : BaseNotifyPropertyChanged, IYouTubeSong, IEquatable<YouTubeSong>
    {
        private string artist;

        private string title;

        private string songId;

        private string originalTitle;

        private Guid songGuid;

        public YouTubeSong()
        {
            this.SongGuid = Guid.NewGuid();
        }

        public YouTubeSong(
            string artist,
            string title,
            string originalTitle,
            string songId,
            string playlistItemId,
            ulong? duration)
            : this()
        {
            this.Artist = artist;
            this.Title = title;
            this.OriginalTitle = originalTitle;
            this.SongId = songId;
            this.PlayListItemId = playlistItemId;
            this.Duration = duration;
        }

        public YouTubeSong(IYouTubeSong iYouTubeSong)
        {
            this.Artist = iYouTubeSong.Artist;
            this.Title = iYouTubeSong.Title;
            this.SongId = iYouTubeSong.SongId;
            this.PlayListItemId = iYouTubeSong.PlayListItemId;
            this.Duration = iYouTubeSong.Duration;
            this.OriginalTitle = iYouTubeSong.OriginalTitle;
            this.SongGuid = iYouTubeSong.SongGuid;
        }

        public string PlayListItemId { get; set; }

        public ulong? Duration { get; set; }

        public string Artist
        {
            get
            {
                return this.artist;
            }

            set
            {
                this.artist = value;
                this.NotifyPropertyChanged();
            }
        }

        public string OriginalTitle
        {
            get
            {
                return this.originalTitle;
            }

            set
            {
                this.originalTitle = value;
                this.NotifyPropertyChanged();
            }
        }

        public Guid SongGuid
        {
            get
            {
                return this.songGuid;
            }

            set
            {
                if (this.songGuid == default(Guid))
                {
                    this.songGuid = value;
                }
            }
        }

        public string SongId
        {
            get
            {
                return this.songId;
            }

            set
            {
                this.songId = value;
                this.NotifyPropertyChanged();
            }
        }

        public string Url
        {
            get
            {
                return string.Concat("https://www.youtube.com/watch?v=", this.SongId);
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }

            set
            {
                this.title = value;
                this.NotifyPropertyChanged();
            }
        }

        public bool IsImported { get; set; }

        public bool Equals(YouTubeSong other)
        {
            return this.SongId.Equals(other.SongId);
        }
    }
}
