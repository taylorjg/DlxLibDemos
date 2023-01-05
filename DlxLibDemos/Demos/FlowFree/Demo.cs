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

  public object[] BuildInternalRows(object demoSettings, CancellationToken cancellationToken)
  {
    var puzzle = (Puzzle)demoSettings;
    var pathFinder = new PathFinder(puzzle);
    var internalRows = new List<FlowFreeInternalRow>();

    foreach (var colourPair in puzzle.ColourPairs)
    {
      _logger.LogInformation($"Finding paths for colour pair {colourPair.Label}...");
      var paths = pathFinder.FindPaths(colourPair, cancellationToken);
      _logger.LogInformation($"Number of paths found for colour pair {colourPair.Label}: {paths.Count}");
      foreach (var path in paths)
      {
        var internalRow = new FlowFreeInternalRow(puzzle, colourPair, path);
        internalRows.Add(internalRow);
      }
    }

    return internalRows.ToArray();
  }

  public int[] InternalRowToMatrixRow(object internalRow)
  {
    var flowFreeInternalRow = internalRow as FlowFreeInternalRow;
    return MakePipeColumns(flowFreeInternalRow);
  }

  public int? GetNumPrimaryColumns(object demoSettings)
  {
    return null;
  }
  public int ProgressFrequency { get => 1000; }

  private static int[] MakePipeColumns(FlowFreeInternalRow internalRow)
  {
    var size = internalRow.Puzzle.Size;
    var columns = Enumerable.Repeat(0, size * size).ToArray();
    foreach (var coords in internalRow.Pipe)
    {
      var index = coords.Row * size + coords.Col;
      columns[index] = 1;
    }
    return columns;
  }
}
