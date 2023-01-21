namespace DlxLibDemos.Demos.Kakuro;

public class KakuroThumbnailWhatToDraw : IWhatToDraw
{
  public object DemoSettings { get; private init; }
  public object DemoDrawingOptions { get; private init; }
  public object[] SolutionInternalRows { get; private init; }

  public KakuroThumbnailWhatToDraw()
  {
    var puzzle = Puzzles.ThePuzzles.First();

    var solution = new[] {
      "..........",
      "..79.54.79",
      ".241.95768",
      ".9587.789.",
      ".12.2469..",
      ".31.629.61",
      "...2153.89",
      "..948.1254",
      ".65347.342",
      ".13.31.13."
    };

    DemoDrawingOptions = true;
    SolutionInternalRows = ParseSolution(puzzle, solution);
  }

  private static KakuroInternalRow[] ParseSolution(Puzzle puzzle, string[] solution)
  {
    var dict = new Dictionary<Coords, int>();

    var size = solution.Length;

    foreach (var row in Enumerable.Range(0, size))
    {
      foreach (var col in Enumerable.Range(0, size))
      {
        var ch = solution[row][col];
        if (int.TryParse(ch.ToString(), out int value))
        {
          var coords = new Coords(row, col);
          dict[coords] = value;
        }
      }
    }

    var internalRowsForHorizontalRuns = puzzle.HorizontalRuns
      .Select(run =>
      {
        var values = run.CoordsList
          .Select(coords => dict[coords])
          .ToArray();
        return new KakuroInternalRow(puzzle, run, values);
      });

    var internalRowsForVerticalRuns = puzzle.VerticalRuns
      .Select(run =>
      {
        var values = run.CoordsList
          .Select(coords => dict[coords])
          .ToArray();
        return new KakuroInternalRow(puzzle, run, values);
      });

    return internalRowsForHorizontalRuns
      .Concat(internalRowsForVerticalRuns)
      .ToArray();
  }
}