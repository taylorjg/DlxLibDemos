using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.Kakuro;

public class KakuroDemo : IDemo
{
  private ILogger<KakuroDemo> _logger;

  public KakuroDemo(ILogger<KakuroDemo> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
  }

  public IDrawable CreateDrawable(IWhatToDraw whatToDraw)
  {
    return new KakuroDrawable(whatToDraw);
  }

  public object[] BuildInternalRows(object demoSettings, CancellationToken cancellationToken)
  {
    return new KakuroInternalRow[0];
  }

  public int[] InternalRowToMatrixRow(object internalRow)
  {
    var kakuroInternalRow = internalRow as KakuroInternalRow;
    return BuildMatrixRow(kakuroInternalRow);
  }

  public int? GetNumPrimaryColumns(object demoSettings)
  {
    return null;
  }

  public int ProgressFrequency { get => 10; }

  private int[] BuildMatrixRow(KakuroInternalRow internalRow)
  {
    return new int[0];
  }
}
