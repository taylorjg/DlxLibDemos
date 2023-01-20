namespace DlxLibDemos.Demos.Nonogram;

public class NonogramThumbnailWhatToDraw : IWhatToDraw
{
  public object DemoSettings { get; private init; }
  public object DemoDrawingOptions { get; private init; }
  public object[] SolutionInternalRows { get; private init; }

  public NonogramThumbnailWhatToDraw()
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
    DemoDrawingOptions = true;
    SolutionInternalRows = ParseSolution(puzzle, solution);
  }

  private static NonogramInternalRow[] ParseSolution(Puzzle puzzle, string[] solution)
  {
    var horizontalRunsDict = ParseHorizontalRuns(puzzle, solution);
    var verticalRunsDict = ParseVerticalRuns(puzzle, solution);

    var solutionInternalRows = new List<NonogramInternalRow>();

    foreach (var runGroup in puzzle.HorizontalRunGroups)
    {
      var horizontalRunGroup = runGroup as HorizontalRunGroup;
      var runCoordsLists = horizontalRunsDict[horizontalRunGroup.Row];
      var internalRow = new NonogramInternalRow(puzzle, horizontalRunGroup, runCoordsLists);
      solutionInternalRows.Add(internalRow);
    }

    foreach (var runGroup in puzzle.VerticalRunGroups)
    {
      var verticalRunGroup = runGroup as VerticalRunGroup;
      var runCoordsLists = verticalRunsDict[verticalRunGroup.Col];
      var internalRow = new NonogramInternalRow(puzzle, verticalRunGroup, runCoordsLists);
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

  private static Dictionary<int, RunCoordsList[]> ParseVerticalRuns(Puzzle puzzle, string[] solution)
  {
    var size = solution.Length;
    var dict = new Dictionary<int, RunCoordsList[]>();

    foreach (var verticalRunGroup in puzzle.VerticalRunGroups)
    {
      var col = (verticalRunGroup as VerticalRunGroup).Col;
      var rows = Enumerable.Range(0, size);
      var overallCoordsList = rows
        .Where(row => solution[row][col] == 'X')
        .Select(row => new Coords(row, col))
        .ToArray();
      var accumulatedLength = 0;
      var runCoordsLists = new List<RunCoordsList>();
      foreach (var length in verticalRunGroup.Lengths)
      {
        var coordsList = overallCoordsList.Skip(accumulatedLength).Take(length).ToArray();
        var runCoordsList = new RunCoordsList(coordsList.ToArray());
        runCoordsLists.Add(runCoordsList);
        accumulatedLength += length;
      }
      dict[col] = runCoordsLists.ToArray();
    }

    return dict;
  }
}
