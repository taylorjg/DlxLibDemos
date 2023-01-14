namespace DlxLibDemos.Demos.Nonogram;

public class NonogramStaticThumbnailWhatToDraw : IWhatToDraw
{
  public object DemoSettings { get; private init; }
  public object DemoOptionalSettings { get; private init; }
  public object[] SolutionInternalRows { get; private init; }

  public NonogramStaticThumbnailWhatToDraw()
  {
    var puzzle = Puzzles.ThePuzzles.First();

    var solution = new[] {
      "...XXXX...",
      "X.XXXXXX..",
      "X..XXXX...",
      "XX..XX....",
      ".XXXXXXXX.",
      "....XX..XX",
      "...XXXX..X",
      "..XX..XX.X",
      "..X....X..",
      "XXX....XXX"
    };

    DemoSettings = puzzle;
    SolutionInternalRows = ParseSolution(puzzle, solution);
  }

  private static NonogramInternalRow[] ParseSolution(Puzzle puzzle, string[] solution)
  {
    var dict = ParseHorizontalRuns(puzzle, solution);

    var solutionInternalRows = new List<NonogramInternalRow>();

    foreach (var index in Enumerable.Range(0, puzzle.HorizontalRunGroups.Length))
    {
      var horizontalRunGroup = puzzle.HorizontalRunGroups[index] as HorizontalRunGroup;
      var runCoordsLists = dict[horizontalRunGroup.Row];
      var internalRow = new NonogramInternalRow(puzzle, horizontalRunGroup, runCoordsLists);
      solutionInternalRows.Add(internalRow);
    }

    return solutionInternalRows.ToArray();
  }

  private static Dictionary<int, RunCoordsList[]> ParseHorizontalRuns(Puzzle puzzle, string[] solution)
  {
    var size = solution.Length;
    var dict = new Dictionary<int, RunCoordsList[]>();

    foreach (var horizontalRunGroup in puzzle.HorizontalRunGroups)
    {
      var row = (horizontalRunGroup as HorizontalRunGroup).Row;
      var cols = Enumerable.Range(0, size);
      var overallCoordsList = cols
        .Where(col => solution[row][col] == 'X')
        .Select(col => new Coords(row, col))
        .ToArray();
      var accumulatedLength = 0;
      var runCoordsLists = new List<RunCoordsList>();
      foreach (var length in horizontalRunGroup.Lengths)
      {
        var coordsList = overallCoordsList.Skip(accumulatedLength).Take(length).ToArray();
        var runCoordsList = new RunCoordsList(coordsList.ToArray());
        runCoordsLists.Add(runCoordsList);
        accumulatedLength += length;
      }
      dict[row] = runCoordsLists.ToArray();
    }

    return dict;
  }
}
