using CloneHeroChartToLuaParser.ClassLibrary.Classes;

namespace CloneHeroChartToLuaParser.ClassLibrary
{
    public static class Parser
    {
        private const string DATA_PATTERN = @"{[\s\S]*?}";
        private const string NOTE_PATTERN = @"(?<Tick>\d+)\s=\s(?<Type>\w)\s(?<Fret>\d)\s(?<Length>\d*)";

        public static Chart ToChart(string pathToChartFile)
        {
            return new Chart();
        }

        public static string ToLuaTable(Chart chart)
        {
            return string.Empty;
        }
    }
}
