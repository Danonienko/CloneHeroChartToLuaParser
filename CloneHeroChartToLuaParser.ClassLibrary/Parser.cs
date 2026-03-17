using CloneHeroChartToLuaParser.ClassLibrary.Classes;
using System.Text.RegularExpressions;

namespace CloneHeroChartToLuaParser.ClassLibrary
{
    public static class Parser
    {
        private const string _MATCH_SECTIONS_PATTERN = @"(?<Section>\[\w*\])\s+(?<Data>{[\s\S]*?})";
        private const string _MATCH_KEY_VALUE_FOR_SONG_SECTION_PATTERN = @"(?<Key>\S+)\s=\s(?<Value>\S+)";
        private const string _MATCH_KEY_VALUE_FOR_SYNC_TRACK_SECTION_PATTERN = @"(?<Tick>\d+)\s=\s(?<Type>\w+)\s(?<Value>\d+)";
        private const string _MATCH_KEY_VALUE_FOR_EVENTS_SECTION_PATTERN = @"(?<Tick>\d+)\s=\s(?<Type>\w+)\s(?<Value>"".+?"")";
        private const string _MATCH_KEY_VALUE_FOR_NOTES_PATTERN = @"(?<Note>(?<Tick>\d+)\s=\s(?<Type>\w+)\s(?<Fret>\d)\s(?<Length>\d+))|(?<Event>(?<Tick>\d+)\s=\s(?<Type>\w+)\s(?<Value>.+))";

        public static Song ReadSongData(string data)
        {
            Dictionary<string, string> keyValuePairs = [];
            List<Match> matches = Regex.Matches(data, _MATCH_KEY_VALUE_FOR_SONG_SECTION_PATTERN).ToList();

            matches.ForEach(match =>
            {
                var key = match.Groups["Key"].Value;
                var value = match.Groups["Value"].Value;
                keyValuePairs.Add(key, value);
            });

            Song song = new()
            {
                Offset = int.Parse(keyValuePairs["Offset"]),
                Resolution = int.Parse(keyValuePairs["Resolution"]),
                Player2 = keyValuePairs["Player2"],
                Difficulty = int.Parse(keyValuePairs["Difficulty"]),
                PreviewStart = int.Parse(keyValuePairs["PreviewStart"]),
                PreviewEnd = int.Parse(keyValuePairs["PreviewEnd"]),
                Genre = keyValuePairs["Genre"],
                MediaType = keyValuePairs["MediaType"],
            };

            return song;
        }

        public static List<SyncTrack> ReadSyncTracks(string data)
        {
            Dictionary<int, (string, int)> keyValuePairs = [];
            List<Match> matches = Regex.Matches(data, _MATCH_KEY_VALUE_FOR_SYNC_TRACK_SECTION_PATTERN).ToList();

            matches.ForEach(match =>
            {
                var tick = int.Parse(match.Groups["Tick"].Value);
                var type = match.Groups["Type"].Value;
                var value = int.Parse(match.Groups["Value"].Value);
                keyValuePairs.Add(tick, (type, value));
            });

            List<SyncTrack> syncTracks = [];
            foreach (var item in keyValuePairs)
            {
                SyncTrack track = new()
                {
                    Tick = item.Key,
                    Type = item.Value.Item1,
                    Value = item.Value.Item2,
                };

                syncTracks.Add(track);
            }

            return syncTracks;
        }

        public static List<Event> ReadEvents(string data)
        {
            Dictionary<int, (string, string)> keyValuePairs = [];
            List<Match> matches = Regex.Matches(data, _MATCH_KEY_VALUE_FOR_EVENTS_SECTION_PATTERN).ToList();

            matches.ForEach(match =>
            {
                var tick = int.Parse(match.Groups["Tick"].Value);
                var type = match.Groups["Type"].Value;
                var value = match.Groups["Value"].Value;
                keyValuePairs.Add(tick, (type, value));
            });

            List<Event> events = [];
            foreach (var item in keyValuePairs)
            {
                Event ev = new()
                {
                    Tick = item.Key,
                    Type = item.Value.Item1,
                    Value = item.Value.Item2,
                };
            }

            return events;
        }

        public static List<Note> ReadNotes(string data)
        {
            throw new NotImplementedException();
        }

        public static Dictionary<string, string> ReadSections(string data)
        {
            var matches = Regex.Matches(data, _MATCH_SECTIONS_PATTERN, RegexOptions.Multiline).ToList();
            Dictionary<string, string> sections = [];
            matches.ForEach(section =>
            {
                var sectionName = section.Groups["Section"].Value;
                var sectionData = section.Groups["Data"].Value;
                sections.Add(sectionName, sectionData);
            });
            return sections;
        }

        public static Chart ToChart(string pathToChartFile)
        {
            var fileContents = File.ReadAllText(pathToChartFile);
            var sections = ReadSections(fileContents);

            Chart chart = new()
            {
                Song = ReadSongData(sections["[Song]"]),
                SyncTracks = ReadSyncTracks(sections["[SyncTrack]"]),
                Events = ReadEvents(sections["[Events]"]),
                ExpertSingle = ReadNotes(sections["[ExpertSingle]"]),
            };

            return chart;
        }

        public static string ToLuaTable(Chart chart)
        {
            throw new NotImplementedException();
        }
    }
}
