using CloneHeroChartToLuaParser.ClassLibrary.Classes;
using System.Text.RegularExpressions;

namespace CloneHeroChartToLuaParser.ClassLibrary
{
    public static class Parser
    {
        private const string _SONG_DATA_PATTERN = @"(?<=\[Song\]\s*){[\s\S]*?}";
        private const string _SYNC_TRACK_PATTERN = @"(?<=\[SyncTrack\]\s*){[\s\S]*?}";
        private const string _EVENTS_PATTERN = @"(?<=\[Events\]\s*){[\s\S]*?}";
        private const string _EXPERT_SINGLE_PATTERN = @"(?<=\[ExpertSingle\]\s*){[\s\S]*?}";
        private const string _NOTE_PATTERN = @"(?<Tick>\d+)\s=\s(?<Type>\w)\s(?<Fret>\d)\s(?<Length>\d*)";

        private static Song _readSongData(string data)
        {
            throw new NotImplementedException();

            var match = Regex.Match(data, @"(?<Key>\S*)\s=\s(?<Value>\S*)", RegexOptions.Multiline);
        }

        private static List<SyncTrack> _readSyncTracks(string data)
        {
            throw new NotImplementedException();
        }

        private static List<Event> _readEvents(string data)
        {
            throw new NotImplementedException();
        }

        private static List<Note> _readNotes(string data)
        {
            throw new NotImplementedException();
        }

        public static Chart ToChart(string pathToChartFile)
        {
            var fileContents = File.ReadAllText(pathToChartFile);

            Chart chart = new()
            {
                Song = _readSongData(Regex.Match(fileContents, _SONG_DATA_PATTERN).Value),
                SyncTracks = _readSyncTracks(Regex.Match(fileContents, _SYNC_TRACK_PATTERN).Value),
                Events = _readEvents(Regex.Match(fileContents, _EVENTS_PATTERN).Value),
                ExpertSingle = _readNotes(Regex.Match(fileContents, _EXPERT_SINGLE_PATTERN).Value)
            };

            return chart;
        }


        public static string ToLuaTable(Chart chart)
        {
            return string.Empty;
        }
    }
}
