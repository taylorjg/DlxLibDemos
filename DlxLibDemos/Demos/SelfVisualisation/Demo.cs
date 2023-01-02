using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.SelfVisualisation;

public class SelfVisualisationDemo : IDemo
{
  private ILogger<SelfVisualisationDemo> _logger;

  public SelfVisualisationDemo(ILogger<SelfVisualisationDemo> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
  }

  public IDrawable CreateDrawable(IWhatToDraw whatToDraw)
  {
    return new SelfVisualisationDrawable(whatToDraw);
  }

  public object[] BuildInternalRows(object demoSettings, CancellationToken cancellationToken)
  {
    var sampleMatrix = (SampleMatrix)demoSettings;
    var matrix = sampleMatrix.Matrix;
    var rowCount = matrix.GetLength(0);
    var colCount = matrix.GetLength(1);

    var makeMatrixRow = (int rowIndex) =>
      Enumerable.Range(0, colCount)
        .Select(colIndex => matrix[rowIndex, colIndex])
        .ToArray();

    return Enumerable.Range(0, rowCount)
      .Select(rowIndex => new SelfVisualisationInternalRow(makeMatrixRow(rowIndex)))
      .ToArray();
  }

  public int[] InternalRowToMatrixRow(object internalRow)
  {
    var selfVisualisationInternalRow = (SelfVisualisationInternalRow)internalRow;
    return selfVisualisationInternalRow.MatrixRow;
  }

  public int? GetNumPrimaryColumns(object demoSettings)
  {
    return null;
  }
}
