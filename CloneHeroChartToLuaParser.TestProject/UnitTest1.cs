using CloneHeroChartToLuaParser.ClassLibrary;
using CloneHeroChartToLuaParser.ClassLibrary.Classes;
using FluentAssertions;

namespace CloneHeroChartToLuaParser.TestProject
{
    public class ParserTests
    {
        public const string FILE_PATH = "Beast and the Harlot.msce";

        public readonly string fileContent = File.ReadAllText(FILE_PATH);

        [SetUp]
        public void Setup()
        {

        }

        [Test]
        public void SectionsShouldBeParsedCorrectly()
        {
            var sections = Parser.ReadSections(fileContent);

            sections.Should().NotBeNullOrEmpty();
            sections.Should().HaveCount(15);
        }

        [Test]
        public void SongSectionShouldBeParsedCorrectly()
        {
            var sections = Parser.ReadSections(fileContent);

            var songSection = sections["[Song]"];
            songSection.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void SongDataShouldBeParsedCorrectly()
        {
            var sections = Parser.ReadSections(fileContent);
            var songSection = sections["[Song]"];

            var songData = Parser.ReadSongData(songSection);
            songData.Should().NotBeNull();
            songData.Offset.Should().Be(0);
            songData.Resolution.Should().Be(480);
            songData.Player2.Should().Be("bass");
            songData.Difficulty.Should().Be(0);
            songData.PreviewStart.Should().Be(0);
            songData.PreviewEnd.Should().Be(0);
            songData.Genre.Should().Be("rock");
            songData.MediaType.Should().Be("cd");
        }

        [Test]
        public void SyncTracksSectionShouldBeParsedCorrectly()
        {
            var sections = Parser.ReadSections(fileContent);

            var syncTracksSection = sections["[SyncTrack]"];
            syncTracksSection.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void SyncTracksDataShouldBeParsedCorrectly()
        {
            var sections = Parser.ReadSections(fileContent);
            var syncTracksSection = sections["[SyncTrack]"];

            var syncTracksData = Parser.ReadSyncTracks(syncTracksSection);
            syncTracksData.Should().NotBeNullOrEmpty();
            syncTracksData.Should().HaveCount(81);

            var syncTrack = syncTracksData[0];
            syncTrack.Tick.Should().Be(0);
            syncTrack.Type.Should().Be("TS");
            syncTrack.Value.Should().Be(6);
        }

        [Test]
        public void EventsSectionShouldBeParsedCorrectly()
        {
            var sections = Parser.ReadSections(fileContent);

            var eventsSection = sections["[Events]"];
            eventsSection.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void EventsDataShouldBeParsedCorrectly()
        {
            var sections = Parser.ReadSections(fileContent);
            var eventsSection = sections["[Events]"];

            var eventsData = Parser.ReadEvents(eventsSection);
            eventsData.Should().NotBeNullOrEmpty();
            eventsData.Should().HaveCount(140);

            var eventData = eventsData[0];
            eventData.Tick.Should().Be(5280);
            eventData.Type.Should().Be("E");
            eventData.Value.Should().Be("phrase_start");
        }

        [Test]
        public void NotesSectionShouldBeParsedCorrectly()
        {
            var sections = Parser.ReadSections(fileContent);
            var notesSection = sections["[ExpertSingle]"];
            notesSection.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void NotesDataShouldBeParsedCorrectly()
        {
            var sections = Parser.ReadSections(fileContent);
            var notesSection = sections["[ExpertSingle]"];

            var notesData = Parser.ReadNotes(notesSection);
            notesData.Should().NotBeNullOrEmpty();

            var eventData = (Event)notesData[0];
            eventData.Tick.Should().Be(5520);
            eventData.Type.Should().Be("E");
            eventData.Value.Should().Be("intense");

            var noteData = (Note)notesData[1];
            noteData.Tick.Should().Be(5760);
            noteData.Type.Should().Be("N");
            noteData.Fret.Should().Be(1);
            noteData.Length.Should().Be(0);
        }

        [Test]
        public void ChartShouldBeParsedCorrectly()
        {
            Chart chart = Parser.ToChart(FILE_PATH);
            chart.Should().NotBeNull();
            chart.Song.Should().NotBeNull();
            chart.SyncTracks.Should().NotBeNullOrEmpty();
            chart.Events.Should().NotBeNullOrEmpty();
            chart.ExpertSingle.Should().NotBeNullOrEmpty();
        }

        [Test]
        public void ParserShouldSerializeCorrectly()
        {
            Chart chart = Parser.ToChart(FILE_PATH);

            string luaTable = Parser.ToLuaTable(chart);
            luaTable.Should().NotBeNullOrEmpty();
            luaTable.Should().Contain("Song");
        }

        [Test]
        public void ParserShouldExportCorrectly()
        {
            string outputPath = "output.lua";

            if (File.Exists(outputPath)) File.Delete(outputPath);
            Parser.Parse(FILE_PATH, outputPath);
            File.Exists(outputPath).Should().BeTrue();
        }
    }
}
