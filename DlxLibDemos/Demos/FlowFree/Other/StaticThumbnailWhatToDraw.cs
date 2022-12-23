namespace DlxLibDemos.Demos.FlowFree;

public class FlowFreeStaticThumbnailWhatToDraw : IWhatToDraw
{
  public object DemoSettings { get; private init; }
  public object DemoOptionalSettings { get; private init; }
  public object[] SolutionInternalRows { get; private init; }

  public FlowFreeStaticThumbnailWhatToDraw()
  {
    var puzzle = Puzzles.ThePuzzles[0];

    DemoSettings = puzzle;
    DemoOptionalSettings = false;

    SolutionInternalRows = new[]
    {
      MakeSolutionInternalRow(puzzle, "A", "0,3 0,2 0,1 1,1 2,1 3,1"),
      MakeSolutionInternalRow(puzzle, "B", "3,4 4,4 4,3 4,2"),
      MakeSolutionInternalRow(puzzle, "C", "0,0 1,0 2,0 3,0 4,0 4,1"),
      MakeSolutionInternalRow(puzzle, "D", "1,3 1,2 2,2"),
      MakeSolutionInternalRow(puzzle, "E", "0,4 1,4 2,4 2,3 3,3 3,2")
    };
  }

  private static FlowFreeInternalRow MakeSolutionInternalRow(
    Puzzle puzzle,
    string label,
    string pipeCoordsListString
  )
  {
    var colourPair = puzzle.ColourPairs.First(cp => cp.Label == label);
    var pipe = ParseCoordsListString(pipeCoordsListString);
    return new FlowFreeInternalRow(puzzle, colourPair, pipe);
  }

  private static Coords[] ParseCoordsListString(string coordsListString)
  {
    return coordsListString.Split(" ", StringSplitOptions.TrimEntries)
      .Select(ParseCoordsString)
      .ToArray();
  }

  private static Coords ParseCoordsString(string coordsString)
  {
    var bits = coordsString.Split(",", StringSplitOptions.TrimEntries);
    var gotRow = int.TryParse(bits[0].ToString(), out int row);
    var gotCol = int.TryParse(bits[1].ToString(), out int col);
    return new Coords(row, col);
  }
}
