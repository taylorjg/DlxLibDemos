using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.RippleEffect;

public class RippleEffectDemo : IDemo
{
  private ILogger<RippleEffectDemo> _logger;

  public RippleEffectDemo(ILogger<RippleEffectDemo> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
  }

  public IDrawable CreateDrawable(IWhatToDraw whatToDraw)
  {
    return new RippleEffectDrawable(whatToDraw);
  }

  public object[] BuildInternalRows(object demoSettings)
  {
    var puzzle = (Puzzle)demoSettings;
    var internalRows = new List<RippleEffectInternalRow>();

    foreach (var initialValue in puzzle.InitialValues)
    {
      var (cell, value) = initialValue;
      var internalRow = new RippleEffectInternalRow(cell, value, true);
      internalRows.Add(internalRow);
    }

    foreach (var room in puzzle.Rooms)
    {
      var givenCells = room.InitialValues.Select(initialValue => initialValue.Cell);
      var givenValues = room.InitialValues.Select(initialValue => initialValue.Value);
      var cellsToSolve = room.Cells.Except(givenCells);
      var values = Enumerable.Range(1, room.Cells.Length);
      var valuesToSolve = values.Except(givenValues).ToArray();

      foreach (var cell in cellsToSolve)
      {
        foreach (var value in valuesToSolve)
        {
          var internalRow = new RippleEffectInternalRow(cell, value, false);
          internalRows.Add(internalRow);
        }
      }
    }

    return internalRows.ToArray();
  }

  public int[] InternalRowToMatrixRow(object internalRow)
  {
    var rippleEffectInternalRow = internalRow as RippleEffectInternalRow;
    var locationColumns = MakeLocationColumns(rippleEffectInternalRow);
    var roomColumns = MakeRoomColumns(rippleEffectInternalRow);
    var rippleColumns = MakeRippleColumns(rippleEffectInternalRow);
    return locationColumns
      .Concat(roomColumns)
      .Concat(rippleColumns)
      .ToArray();
  }

  public int? GetNumPrimaryColumns(object demoSettings)
  {
    return 2 * 8 * 8;
  }

  private static readonly Coords[] AllLocations =
    Enumerable.Range(0, 8).SelectMany(row =>
        Enumerable.Range(0, 8).Select(col =>
          new Coords(row, col))).ToArray();

  private static int[] MakeLocationColumns(RippleEffectInternalRow internalRow)
  {
    var columns = Enumerable.Repeat(0, 8 * 8).ToArray();
    return columns;
  }

  private static int[] MakeRoomColumns(RippleEffectInternalRow internalRow)
  {
    var columns = Enumerable.Repeat(0, 8 * 8).ToArray();
    return columns;
  }

  private static int[] MakeRippleColumns(RippleEffectInternalRow internalRow)
  {
    var columns = Enumerable.Repeat(0, 4 * 8 * 8).ToArray();
    return columns;
  }
}
