using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.NQueens;

public class NQueensDemo : IDemo
{
  private ILogger<NQueensDemo> _logger;
  private int N;

  public NQueensDemo(ILogger<NQueensDemo> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
  }

  public IDrawable CreateDrawable(IWhatToDraw whatToDraw)
  {
    return new NQueensDrawable(whatToDraw);
  }

  public object[] BuildInternalRows(object demoSettings)
  {
    N = (int)demoSettings;
    var locations =
      Enumerable.Range(0, N).SelectMany(row =>
        Enumerable.Range(0, N).Select(col =>
          new Coords(row, col))).ToArray();
    return locations.Select(coords => new NQueensInternalRow(coords)).ToArray();
  }

  public int[] InternalRowToMatrixRow(object internalRow)
  {
    var nQueensInternalRow = internalRow as NQueensInternalRow;
    return BuildMatrixRow(nQueensInternalRow);
  }

  public int? GetNumPrimaryColumns(object demoSettings)
  {
    var N = (int)demoSettings;
    return N + N;
  }

  private int[] BuildMatrixRow(NQueensInternalRow internalRow)
  {
    var row = internalRow.Coords.Row;
    var col = internalRow.Coords.Col;
    var diagonalColumnCount = N + N - 3;

    var rowColumns = Enumerable.Repeat(0, N).ToArray();
    var colColumns = Enumerable.Repeat(0, N).ToArray();
    var diagonal1Columns = Enumerable.Repeat(0, diagonalColumnCount).ToArray();
    var diagonal2Columns = Enumerable.Repeat(0, diagonalColumnCount).ToArray();

    rowColumns[row] = 1;
    colColumns[col] = 1;

    var diagonal1 = row + col - 1;
    if (diagonal1 >= 0 && diagonal1 < diagonalColumnCount) diagonal1Columns[diagonal1] = 1;

    var diagonal2 = N - 1 - col + row - 1;
    if (diagonal2 >= 0 && diagonal2 < diagonalColumnCount) diagonal2Columns[diagonal2] = 1;

    return rowColumns
      .Concat(colColumns)
      .Concat(diagonal1Columns)
      .Concat(diagonal2Columns)
      .ToArray();
  }
}
