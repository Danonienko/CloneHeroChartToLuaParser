# Clone Hero Chart to Lua Parser

A C# library and command-line tool that parses Clone Hero chart files (.msce format) and converts them to Lua table format. This tool is useful for extracting chart data from Clone Hero song charts and exporting them in a format that can be used with Lua-based applications.

## Overview

Clone Hero is a popular rhythm game that uses chart files to define songs. Chart files contain structured data about the song metadata, sync track information, note placements across different difficulty levels, and events. This project provides tools to parse these chart files and serialize them into Lua tables for use in other applications.

## Features

- **Chart File Parsing**: Reads and parses Clone Hero `.msce` chart files
- **Data Extraction**: Extracts song metadata, sync tracks, notes, and events
- **Lua Serialization**: Converts parsed chart data into Lua table format
- **Multiple Difficulty Levels**: Supports Guitar and Bass tracks with Easy, Medium, Hard, and Expert difficulty levels
- **Command-Line Interface**: Easy-to-use console application for batch processing

## Project Structure

```
CloneHeroChartToLuaParser.ClassLibrary/
  ├── Parser.cs           # Main parsing logic for chart files
  ├── LuaSerializer.cs    # Serializes objects to Lua table format
  └── Classes/
      ├── Chart.cs        # Main chart data model
      ├── Song.cs         # Song metadata
      ├── SyncTrack.cs    # Sync track information
      ├── Event.cs        # Chart events
      └── Note.cs         # Individual note data

CloneHeroChartToLuaParser.ConsoleApp/
  └── Program.cs          # Command-line interface

CloneHeroChartToLuaParser.TestProject/
  └── UnitTest1.cs        # Unit tests for parser functionality
```

## Data Models

### Chart
Main container for all chart data, including:
- `Song`: Metadata about the song
- `SyncTracks`: List of tempo/time signature changes
- Difficulty tracks: `ExpertSingle`, `HardSingle`, `MediumSingle`, `EasySingle`, `ExpertDoubleBass`, `HardDoubleBass`, `MediumDoubleBass`, `EasyDoubleBass`
- `Events`: Special events triggered during gameplay

### Song
Song metadata including:
- `Offset`: Audio offset in milliseconds
- `Resolution`: PPQN (Parts Per Quarter Note)
- `Difficulty`: Difficulty rating
- `PreviewStart` / `PreviewEnd`: Preview timing
- `Genre`: Song genre
- `MediaType`: Audio file type
- `Player2`: Player 2 track setting

### Note
Individual note data:
- `Tick`: Position in the chart
- `Type`: Note type identifier
- `Fret`: Fret number (0-5)
- `Length`: Duration of the note

### SyncTrack
Timing information:
- `Tick`: Position in the chart
- `Type`: Sync event type
- `Value`: Tempo or time signature value

### Event
Chart events:
- `Tick`: Position in the chart
- `Type`: Event type
- `Value`: Event-specific data

## Usage

### Command-Line Interface

```bash
CloneHeroChartToLuaParser.ConsoleApp <path_to_chart_file> <output_path>
```

**Example:**
```bash
CloneHeroChartToLuaParser.ConsoleApp "song_chart.msce" "output.lua"
```

This will parse the chart file and output a Lua table representation to the specified output file.

### Library Usage

```csharp
using CloneHeroChartToLuaParser.ClassLibrary;
using CloneHeroChartToLuaParser.ClassLibrary.Classes;

// Parse a chart file
Parser.Parse("song_chart.msce", "output.lua");

// Or parse sections manually
string chartContent = File.ReadAllText("song_chart.msce");
var sections = Parser.ReadSections(chartContent);

// Parse specific sections
var songData = Parser.ReadSongData(sections["[Song]"]);
var syncTrack = Parser.ReadSyncTrack(sections["[SyncTrack]"]);
```

## Output Format

The parser outputs a Lua table structure that can be directly used in Lua applications:

```lua
{
  Song = {
    Offset = 1000,
    Resolution = 192,
    Difficulty = 5,
    PreviewStart = 0,
    PreviewEnd = 30000,
    Genre = "Rock",
    MediaType = "mp3",
    Player2 = "Bass"
  },
  SyncTracks = { ... },
  Events = { ... },
  ExpertSingle = { ... },
  -- ... other difficulty tracks
}
```

## Building

This project uses .NET and can be built with Visual Studio or the .NET CLI:

```bash
dotnet build
```

## Testing

Run tests with:

```bash
dotnet test
```

Test files include sample chart files (e.g., `Beast and the Harlot.msce`) to verify parsing correctness.

## Dependencies

- .NET 6.0 or higher
- FluentAssertions (for testing)

## License

Please check the project for license information.

## Contributing

Contributions are welcome! Please feel free to submit pull requests or open issues for bugs and feature requests.