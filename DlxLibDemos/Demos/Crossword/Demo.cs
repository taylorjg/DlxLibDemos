using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.Crossword;

public class CrosswordDemo : IDemo
{
  private ILogger<CrosswordDemo> _logger;

  public CrosswordDemo(ILogger<CrosswordDemo> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
  }

  public IDrawable CreateDrawable(IWhatToDraw whatToDraw)
  {
    return new CrosswordDrawable(whatToDraw);
  }

  public object[] BuildInternalRows(object demoSettings, CancellationToken cancellationToken)
  {
    var internalRows = new List<CrosswordInternalRow>();

    return internalRows.ToArray();
  }

  public int[] InternalRowToMatrixRow(object internalRow)
  {
    return new int[0];
  }

  public int? GetNumPrimaryColumns(object demoSettings)
  {
    return null;
  }

  public int ProgressFrequency { get => 10; }
}
