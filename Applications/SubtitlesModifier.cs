using System;
using System.Globalization;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Foundation.Abstracts;

namespace Applications
{
    public class SubtitlesModifier : ApplicationExecutable<SubtitlesModifier>
    {
        private string[] SubtitleFile { get; set; }
        private string FilePath { get; set; }
        private int TimingAdjust { get; set; }
        private static Regex TimingPattern => new Regex(@"(\d+:\d+:\d+,\d+) --> (\d+:\d+:\d+,\d+)", RegexOptions.Compiled | RegexOptions.IgnoreCase);

        public override void ReceiveParameters(params object[] parameters)
        {
            return;
        }

        public void SetSubtitleTimeAdjust(int timeAdjust) => TimingAdjust = timeAdjust;

        public async Task LoadSubtitleFile(string path)
        {
            FilePath = path;
            SubtitleFile = await File.ReadAllLinesAsync(path);
        }

        public override async Task<object> Execute()
        {
            Console.WriteLine("Starting");
            if(SubtitleFile.Length <= 0 || TimingAdjust == 0)
            {
                Console.WriteLine("No file and/or timing provided");
                return Task.FromResult(false);
            }
            var writer = new StringBuilder();
            foreach (var line in SubtitleFile)
            {
                var times = TimingPattern.Matches(line);
                if (times.Count > 0)
                {
                    var start = DateTime.ParseExact(times[0].Groups[1].Value, "HH:mm:ss,fff", CultureInfo.InvariantCulture).AddSeconds(TimingAdjust);
                    var end = DateTime.ParseExact(times[0].Groups[2].Value, "HH:mm:ss,fff", CultureInfo.InvariantCulture).AddSeconds(TimingAdjust);
                    writer.AppendLine($"{start:HH:mm:ss,fff} --> {end:HH:mm:ss,fff}");
                }
                else
                    writer.AppendLine(line);
            }

            var adjustedFilename = $"{FilePath}_adjusted{TimingAdjust}s.srt";
            await File.WriteAllTextAsync(adjustedFilename, writer.ToString());
            Console.WriteLine("Done.");
            return Task.FromResult(true);
        }
    }
}