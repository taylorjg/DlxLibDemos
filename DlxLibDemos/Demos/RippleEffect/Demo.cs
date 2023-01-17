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

  public object[] BuildInternalRows(object demoSettings, CancellationToken cancellationToken)
  {
    var puzzle = (Puzzle)demoSettings;
    var internalRows = new List<RippleEffectInternalRow>();

    foreach (var room in puzzle.Rooms)
    {
      foreach (var initialValue in room.InitialValues)
      {
        var (cell, value) = initialValue;
        var internalRow = new RippleEffectInternalRow(puzzle, cell, value, true, room);
        internalRows.Add(internalRow);
      }

      var givenCells = room.InitialValues.Select(initialValue => initialValue.Cell);
      var givenValues = room.InitialValues.Select(initialValue => initialValue.Value);
      var cellsToSolve = room.Cells.Except(givenCells);
      var values = Enumerable.Range(1, room.Cells.Length);
      var valuesToSolve = values.Except(givenValues).ToArray();

      foreach (var cell in cellsToSolve)
      {
        foreach (var value in valuesToSolve)
        {
          var internalRow = new RippleEffectInternalRow(puzzle, cell, value, false, room);
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
    var puzzle = (Puzzle)demoSettings;
    var size = puzzle.Size;
    return 2 * size * size;
  }

  public int ProgressFrequency { get => 1; }

  private static int[] MakeLocationColumns(RippleEffectInternalRow internalRow)
  {
    var size = internalRow.Puzzle.Size;
    var columns = Enumerable.Repeat(0, size * size).ToArray();
    var (row, col) = internalRow.Cell;
    columns[row * size + col] = 1;
    return columns;
  }

  private static int[] MakeRoomColumns(RippleEffectInternalRow internalRow)
  {
    var size = internalRow.Puzzle.Size;
    var columns = Enumerable.Repeat(0, size * size).ToArray();
    var index = internalRow.Room.StartIndex + internalRow.Value - 1;
    columns[index] = 1;
    return columns;
  }

  private static int[] MakeRippleColumns(RippleEffectInternalRow internalRow)
  {
    var size = internalRow.Puzzle.Size;
    var maxValue = internalRow.Puzzle.MaxValue;

    var arrayOfArrays = Enumerable.Range(0, maxValue * 4).Select(_ => new int[size * size]).ToArray();

    var baseIndex = (internalRow.Value - 1) * 4;

    var setRippleColumnBits = (int baseIndexOffset, Func<Coords, Coords> transformCoords) =>
    {
      var rippleCells = GetRippleCells(internalRow, transformCoords);
      var array = arrayOfArrays[baseIndex + baseIndexOffset];
      foreach (var cell in rippleCells)
      {
        var index = cell.Row * size + cell.Col;
        array[index] = 1;
      }
    };

    setRippleColumnBits(0, cell => cell.Up());
    setRippleColumnBits(1, cell => cell.Down());
    setRippleColumnBits(2, cell => cell.Left());
    setRippleColumnBits(3, cell => cell.Right());

    return arrayOfArrays.SelectMany(array => array).ToArray();
  }

  private static Coords[] GetRippleCells(
    RippleEffectInternalRow internalRow,
    Func<Coords, Coords> transformCoords
  )
  {
    var size = internalRow.Puzzle.Size;
    var cell = internalRow.Cell;
    var value = internalRow.Value;

    var cells = new List<Coords> { cell };

    foreach (var _ in Enumerable.Range(0, value))
    {
      cell = transformCoords(cell);
      if (cell.Row >= 0 && cell.Row < size && cell.Col >= 0 && cell.Col < size)
      {
        cells.Add(cell);
      }
    }

    return cells.ToArray();
  }
}
