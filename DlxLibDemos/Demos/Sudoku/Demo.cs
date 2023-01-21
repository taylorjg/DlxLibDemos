using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.Sudoku;

public class SudokuDemo : IDemo
{
  private ILogger<SudokuDemo> _logger;

  public SudokuDemo(ILogger<SudokuDemo> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
  }

  public IDrawable CreateDrawable(IWhatToDraw whatToDraw)
  {
    return new SudokuDrawable(whatToDraw);
  }

  public object[] BuildInternalRows(object demoSettings, CancellationToken cancellationToken)
  {
    var puzzle = (Puzzle)demoSettings;
    var initialValues = puzzle.InitialValues;
    return AllCoords.SelectMany(coords =>
    {
      var initialValue = initialValues.FirstOrDefault(iv => iv.Coords == coords);
      return (initialValue != null)
        ? BuildInternalRowsForInitialValue(initialValue)
        : BuildInternalRowsForCoords(coords);
    }).ToArray();
  }

  public int[] InternalRowToMatrixRow(object internalRow)
  {
    var sudokuInternalRow = internalRow as SudokuInternalRow;
    return BuildMatrixRow(sudokuInternalRow);
  }

  public int? GetNumPrimaryColumns(object demoSettings)
  {
    return null;
  }

  public int ProgressFrequency { get => 1; }

  private static readonly Coords[] AllCoords =
    Enumerable.Range(0, 9).SelectMany(row =>
        Enumerable.Range(0, 9).Select(col =>
          new Coords(row, col))).ToArray();

  private static readonly int[] AllValues = Enumerable.Range(1, 9).ToArray();

  private SudokuInternalRow[] BuildInternalRowsForInitialValue(InitialValue initialValue)
  {
    return new[] { new SudokuInternalRow(initialValue.Coords, initialValue.Value, true) };
  }

  private SudokuInternalRow[] BuildInternalRowsForCoords(Coords coords)
  {
    return AllValues.Select(value => new SudokuInternalRow(coords, value, false)).ToArray();
  }

  private int[] BuildMatrixRow(SudokuInternalRow internalRow)
  {
    var zeroBasedValue = internalRow.Value - 1;
    var row = internalRow.Coords.Row;
    var col = internalRow.Coords.Col;
    var box = RowColToBox(row, col);
    var posColumns = OneHot(row, col);
    var rowColumns = OneHot(row, zeroBasedValue);
    var colColumns = OneHot(col, zeroBasedValue);
    var boxColumns = OneHot(box, zeroBasedValue);
    return posColumns
      .Concat(rowColumns)
      .Concat(colColumns)
      .Concat(boxColumns)
      .ToArray();
  }

  private int RowColToBox(int row, int col)
  {
    return row - (row % 3) + (col / 3);
  }

  private int[] OneHot(int major, int minor)
  {
    var columns = Enumerable.Repeat(0, 81).ToArray();
    columns[major * 9 + minor] = 1;
    return columns;
  }
}
