namespace DlxLibDemos.Demos.RippleEffect;

public class RippleEffectStaticThumbnailWhatToDraw : IWhatToDraw
{
  public object DemoSettings { get; private init; }
  public object DemoOptionalSettings { get; private init; }
  public object[] SolutionInternalRows { get; private init; }

  public RippleEffectStaticThumbnailWhatToDraw()
  {
    var puzzle = Puzzles.ThePuzzles.First();

    var solution = new[] {
      "23124521",
      "61312432",
      "42131213",
      "35243121",
      "13121345",
      "21314231",
      "12432513",
      "34123121"
    };

    DemoSettings = puzzle;
    SolutionInternalRows = ParseSolution(puzzle, solution);
  }

  private static RippleEffectInternalRow[] ParseSolution(Puzzle puzzle, string[] solution)
  {
    var solutionInternalRows = new List<RippleEffectInternalRow>();

    var size = solution.Length;

    foreach (var row in Enumerable.Range(0, size))
    {
      foreach (var col in Enumerable.Range(0, size))
      {
        var ch = solution[row][col];
        if (int.TryParse(ch.ToString(), out int value))
        {
          var cell = new Coords(row, col);
          var isInitialValue = puzzle.InitialValues.Any(initialValue => initialValue.Cell == cell);
          var room = puzzle.Rooms.First(room => room.Cells.Contains(cell));
          var roomIndex = room.RoomStartIndex;
          var solutionInternalRow = new RippleEffectInternalRow(
            puzzle,
            cell,
            value,
            isInitialValue,
            roomIndex);
          solutionInternalRows.Add(solutionInternalRow);
        }
      }
    }

    return solutionInternalRows.ToArray();
  }
}
