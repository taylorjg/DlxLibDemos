using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.Nonogram;

public class NonogramDemo : IDemo
{
  private ILogger<NonogramDemo> _logger;

  public NonogramDemo(ILogger<NonogramDemo> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
  }

  public IDrawable CreateDrawable(IWhatToDraw whatToDraw)
  {
    return new NonogramDrawable(whatToDraw);
  }

  public object[] BuildInternalRows(object demoSettings, CancellationToken cancellationToken)
  {
    var puzzle = Puzzles.ThePuzzles.First();
    var internalRows = new List<NonogramInternalRow>();

    return internalRows.ToArray();
  }

  public int[] InternalRowToMatrixRow(object internalRow)
  {
    var nonogramInternalRow = internalRow as NonogramInternalRow;
    return new int[0];
  }

  public int? GetNumPrimaryColumns(object demoSettings)
  {
    var puzzle = Puzzles.ThePuzzles.First();
    return puzzle.HorizontalRuns.Length + puzzle.VerticalRuns.Length;
  }

  public int ProgressFrequency { get => 10; }
}
