using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace YouTubePlaylistAPI
{
    public static class SongTitleParser
    {
        private static readonly string RegexSongPattern = @"\s*(?<Artist>[a-zA-Z1-9\s\w]{1,})-(?<Name>[a-zA-Z1-9\-\s\w""']{1,})";

        public static KeyValuePair<string, string> ParseTitle(string text)
        {
            Regex regexNamespaceInitializations = new Regex(RegexSongPattern, RegexOptions.None);

            Match m = regexNamespaceInitializations.Match(text);
            KeyValuePair<string, string> currentSong = default(KeyValuePair<string, string>);
            if (m.Success)
            {
                currentSong = new KeyValuePair<string, string>(m.Groups["Artist"].ToString(), m.Groups["Name"].ToString());
            }
            else
            {
                currentSong = new KeyValuePair<string, string>(text, string.Empty);
            }

            return currentSong;
        }
    }
}