using CloneHeroChartToLuaParser.ClassLibrary;

namespace CloneHeroChartToLuaParser.ConsoleApp
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            if (args.Length != 2)
            {
                Console.WriteLine("Usage: CloneHeroChartToLuaParser.ConsoleApp <path_to_chart_file> <output_path>");
                return;
            }

            string pathToChartFile = args[0];
            string outputPath = args[1];

            try
            {
                Parser.Parse(pathToChartFile, outputPath);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
            }
        }
    }
}
