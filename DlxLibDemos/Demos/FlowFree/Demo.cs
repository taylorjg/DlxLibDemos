using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.FlowFree;

public class FlowFreeDemo : IDemo
{
  private ILogger<FlowFreeDemo> _logger;

  public FlowFreeDemo(ILogger<FlowFreeDemo> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
  }

  public IDrawable CreateDrawable(IWhatToDraw whatToDraw)
  {
    return new FlowFreeDrawable(whatToDraw);
  }

  public object[] BuildInternalRows(object demoSettings)
  {
    return new object[0];
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
