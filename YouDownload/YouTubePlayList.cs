namespace YouTubePlaylistAPI
{
    public class YouTubePlayList
    {
        public YouTubePlayList(string name, string id)
        {
            this.Name = name;
            this.Id = id;
        }

        public string Name { get; set; }

        public string Id { get; set; }
    }
}