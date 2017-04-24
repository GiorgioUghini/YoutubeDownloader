using System;
using System.Text.RegularExpressions;

namespace YouTubePlaylistAPI
{
    public class DurationParser
    {
        private readonly string durationRegexExpression = @"PT(?<minutes>[0-9]{0,})M(?<seconds>[0-9]{0,})S";

        public ulong? GetDuration(string durationStr)
        {
            ulong? durationResult = default(ulong?);
            Regex regexNamespaceInitializations = new Regex(durationRegexExpression, RegexOptions.None);
            Match m = regexNamespaceInitializations.Match(durationStr);
            if (m.Success)
            {
                string minutesStr = m.Groups["minutes"].Value;
                string secondsStr = m.Groups["seconds"].Value;
                int minutes = int.Parse(minutesStr);
                int seconds = int.Parse(secondsStr);
                TimeSpan duration = new TimeSpan(0, minutes, seconds);
                durationResult = (ulong)duration.Ticks;
            }

            return durationResult;
        }
    }
}