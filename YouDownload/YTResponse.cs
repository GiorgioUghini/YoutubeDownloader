namespace YouDownload
{
    public class YTResponse
    {
        public string nextPageToken { get; set; }
        public YTResponseItem[] items { get; set; }
    }

    public class YTResponseItem
    {
        public YTResponseContentDetails contentDetails { get; set; }
    }

    public class YTResponseContentDetails
    {
        public string videoId { get; set; }
    }
}
