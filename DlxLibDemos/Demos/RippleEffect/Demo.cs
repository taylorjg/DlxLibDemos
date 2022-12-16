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

  private static readonly Coords[] Locations =
    Enumerable.Range(0, 8).SelectMany(row =>
        Enumerable.Range(0, 8).Select(col =>
          new Coords(row, col))).ToArray();
}
