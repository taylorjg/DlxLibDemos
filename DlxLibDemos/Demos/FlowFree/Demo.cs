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
    var puzzle = (Puzzle)demoSettings;
    var pathFinder = new PathFinder(puzzle);
    var internalRows = new List<FlowFreeInternalRow>();
    foreach (var colourPair in puzzle.ColourPairs)
    {
      var path = pathFinder.FindPath(colourPair);
      if (path != null) {
        var internalRow = new FlowFreeInternalRow(puzzle, colourPair, path);
        _logger.LogInformation($"colourPair: {colourPair.Label}");
        foreach (var coords in path) {
          _logger.LogInformation($"coords: {coords}");
        }
        internalRows.Add(internalRow);
      }
    }
    return internalRows.ToArray();
  }

  public int[] InternalRowToMatrixRow(object internalRow)
  {
    var flowFreeInternalRow = internalRow as FlowFreeInternalRow;
    var colourPairColumns = MakeColourPairColumns(flowFreeInternalRow);
    var pipeColumns = MakePipeColumns(flowFreeInternalRow);
    return colourPairColumns.Concat(pipeColumns).ToArray();
  }

  public int? GetNumPrimaryColumns(object demoSettings)
  {
    return null;
  }

  private static int[] MakeColourPairColumns(FlowFreeInternalRow internalRow)
  {
    var numColourPairs = internalRow.Puzzle.ColourPairs.Length;
    var columns = Enumerable.Repeat(0, numColourPairs).ToArray();
    var index = Array.FindIndex(internalRow.Puzzle.ColourPairs, cp => cp == internalRow.ColourPair);
    columns[index] = 1;
    return columns;
  }

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
