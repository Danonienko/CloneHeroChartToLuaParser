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
                Genre = keyValuePairs["Genre"].Replace("\"", string.Empty),
                MediaType = keyValuePairs["MediaType"].Replace("\"", string.Empty),
            };

            return song;
        }

        public static List<SyncTrack> ReadSyncTracks(string data)
        {
            List<Match> matches = Regex.Matches(data, _MATCH_KEY_VALUE_FOR_SYNC_TRACK_SECTION_PATTERN).ToList();

            List<SyncTrack> syncTracks = [];
            matches.ForEach(match =>
            {
                SyncTrack track = new()
                {
                    Tick = int.Parse(match.Groups["Tick"].Value),
                    Type = match.Groups["Type"].Value,
                    Value = int.Parse(match.Groups["Value"].Value)
                };

                syncTracks.Add(track);
            });

            return syncTracks;
        }

        public static List<Event> ReadEvents(string data)
        {
            List<Match> matches = Regex.Matches(data, _MATCH_KEY_VALUE_FOR_EVENTS_SECTION_PATTERN).ToList();

            List<Event> events = [];
            matches.ForEach(match =>
            {
                Event @event = new()
                {
                    Tick = int.Parse(match.Groups["Tick"].Value),
                    Type = match.Groups["Type"].Value,
                    Value = match.Groups["Value"].Value.Replace("\"", string.Empty)
                };

                events.Add(@event);
            });

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
