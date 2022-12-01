using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.TetraSticks;

public class TetraSticksDemo : IDemo
{
  private ILogger<TetraSticksDemo> _logger;

  public TetraSticksDemo(ILogger<TetraSticksDemo> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
  }
  public IDrawable CreateDrawable(IWhatToDraw whatToDraw)
  {
    return new TetraSticksDrawable(whatToDraw);
  }

  public object[] BuildInternalRows(object demoSettings)
  {
    return null;
  }

  public int[] InternalRowToMatrixRow(object internalRow)
  {
    return null;
  }

  public int? GetNumPrimaryColumns(object demoSettings)
  {
    return null;
  }
}
