namespace CloneHeroChartToLuaParser.ClassLibrary.Classes
{
    public class Chart
    {
        public Song Song { get; set; }
        public List<SyncTrack> SyncTracks { get; set; } = [];
        public List<Event> Events { get; set; } = [];

        // Guitar
        public List<object> ExpertSingle { get; set; } = [];
        public List<object> HardSingle { get; set; } = [];
        public List<object> MediumSingle { get; set; } = [];
        public List<object> EasySingle { get; set; } = [];

        // Bass
        public List<object> ExpertDoubleBass { get; set; } = [];
        public List<object> HardDoubleBass { get; set; } = [];
        public List<object> MediumDoubleBass { get; set; } = [];
        public List<object> EasyDoubleBass { get; set; } = [];
    }
}
