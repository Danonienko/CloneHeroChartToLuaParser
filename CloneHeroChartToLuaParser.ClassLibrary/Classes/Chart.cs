namespace CloneHeroChartToLuaParser.ClassLibrary.Classes
{
    public class Chart
    {
        public Song Song { get; set; }
        public List<SyncTrack> SyncTracks { get; set; } = [];
        public List<Event> Events { get; set; } = [];

        // Guitar
        public List<Note> ExpertSingle { get; set; } = [];
        public List<Note> HardSingle { get; set; } = [];
        public List<Note> MediumSingle { get; set; } = [];
        public List<Note> EasySingle { get; set; } = [];

        // Bass
        public List<Note> ExpertDoubleBass { get; set; } = [];
        public List<Note> HardDoubleBass { get; set; } = [];
        public List<Note> MediumDoubleBass { get; set; } = [];
        public List<Note> EasyDoubleBass { get; set; } = [];
    }
}
