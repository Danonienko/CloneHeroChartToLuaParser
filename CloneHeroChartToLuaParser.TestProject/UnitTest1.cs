using CloneHeroChartToLuaParser.ClassLibrary;
using FluentAssertions;

namespace CloneHeroChartToLuaParser.TestProject
{
    public class ParserTests
    {
        public const string FILE_PATH = @"C:\Users\Daniel\Documents\Beast and the Harlot.msce";

        public readonly string fileContent = File.ReadAllText(FILE_PATH);

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void SectionsShouldNotBeEmpty()
        {
            var sections = Parser.ReadSections(fileContent);

            sections.Should().NotBeEmpty();
        }
    }
}
