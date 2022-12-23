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

  public object[] BuildInternalRows(object demoSettings)
  {
    return new SelfVisualisationInternalRow[0];
  }

  public int[] InternalRowToMatrixRow(object internalRow)
  {
    return new int[0];
  }

  public int? GetNumPrimaryColumns(object demoSettings)
  {
    return null;
  }
}
