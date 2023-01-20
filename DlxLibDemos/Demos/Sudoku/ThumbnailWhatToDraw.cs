namespace DlxLibDemos.Demos.Sudoku;

public class SudokuThumbnailWhatToDraw : IWhatToDraw
{
  public object DemoSettings { get; private init; }
  public object DemoDrawingOptions { get; private init; }
  public object[] SolutionInternalRows { get; private init; }

  public SudokuThumbnailWhatToDraw()
  {
    var puzzle = Puzzles.ThePuzzles.First();

    var solution = new[] {
      "614892753",
      "893517264",
      "257346918",
      "432185679",
      "569274381",
      "781639542",
      "375428196",
      "146953827",
      "928761435"
    };

    DemoSettings = puzzle;
    SolutionInternalRows = ParseSolution(puzzle, solution);
  }

  private static SudokuInternalRow[] ParseSolution(Puzzle puzzle, string[] solution)
  {
    var solutionInternalRows = new List<SudokuInternalRow>();

    var size = solution.Length;

    foreach (var row in Enumerable.Range(0, size))
    {
      foreach (var col in Enumerable.Range(0, size))
      {
        var ch = solution[row][col];
        if (int.TryParse(ch.ToString(), out int value))
        {
          var coords = new Coords(row, col);
          var isInitialValue = puzzle.InternalRows.Any(internalRow => internalRow.Coords == coords);
          var solutionInternalRow = new SudokuInternalRow(coords, value, isInitialValue);
          solutionInternalRows.Add(solutionInternalRow);
        }
      }
    }

    return solutionInternalRows.ToArray();
  }
}
