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

    foreach (var room in puzzle.Rooms)
    {
      var findRoomCellIndex = (Coords cell) =>
      {
        var indexWithinRoom = Array.FindIndex(room.Cells, c => c == cell);
        return room.RoomStartIndex + indexWithinRoom;
      };

      foreach (var initialValue in room.InitialValues)
      {
        var (cell, value) = initialValue;
        var roomCellIndex = findRoomCellIndex(cell);
        var internalRow = new RippleEffectInternalRow(cell, value, true, roomCellIndex);
        internalRows.Add(internalRow);
      }

      var givenCells = room.InitialValues.Select(initialValue => initialValue.Cell);
      var givenValues = room.InitialValues.Select(initialValue => initialValue.Value);
      var cellsToSolve = room.Cells.Except(givenCells);
      var values = Enumerable.Range(1, room.Cells.Length);
      var valuesToSolve = values.Except(givenValues).ToArray();

      foreach (var cell in cellsToSolve)
      {
        var roomCellIndex = findRoomCellIndex(cell);
        foreach (var value in valuesToSolve)
        {
          var internalRow = new RippleEffectInternalRow(cell, value, false, roomCellIndex);
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
    var (row, col) = internalRow.Cell;
    columns[row * 8 + col] = 1;
    return columns;
  }

  private static int[] MakeRoomColumns(RippleEffectInternalRow internalRow)
  {
    var columns = Enumerable.Repeat(0, 8 * 8).ToArray();
    columns[internalRow.RoomCellIndex] = 1;
    return columns;
  }

  private static int[] MakeRippleColumns(RippleEffectInternalRow internalRow)
  {
    var size = 8;
    var maxValue = 5;

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

  private static Coords[] GetRippleCells(RippleEffectInternalRow internalRow, Func<Coords, Coords> transformCoords)
  {
    var (cell, value, _isInitialValue, _roomCellIndex) = internalRow;
    var cells = new List<Coords> { cell };
    var size = 8;
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
