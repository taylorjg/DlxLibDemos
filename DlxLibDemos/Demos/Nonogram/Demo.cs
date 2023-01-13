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

    var intsToString = (IEnumerable<int> ns) => "[" + string.Join(", ", ns.Select(n => n.ToString())) + "]";

    var size = puzzle.Size;

    foreach (var horizontalRunGroup in puzzle.HorizontalRunGroups)
    {
      var setsOfStartingPositions = new List<int[]>();
      var workingSetOfStartingPositions = new Stack<int>();

      void RecursivelyFindSetsOfStartingPositions(int startPosition, int[] remainingLengths)
      {
        if (remainingLengths.Length == 0)
        {
          if (workingSetOfStartingPositions.Count == horizontalRunGroup.Lengths.Length)
          {
            var setOfStartingPositions = workingSetOfStartingPositions.Reverse().ToArray();
            setsOfStartingPositions.Add(setOfStartingPositions);
          }
          return;
        }

        var runLength = remainingLengths[0];
        var newRemainingLengths = remainingLengths[1..];
        var sumOfRemainingLengths = newRemainingLengths.Sum();
        var requiredGaps = newRemainingLengths.Length;
        var lastValidStartPosition = size - sumOfRemainingLengths - requiredGaps - runLength;
        var numValidStartPositions = lastValidStartPosition - startPosition + 1;
        var validStartPositions = Enumerable.Range(startPosition, numValidStartPositions);

        foreach (var validStartPosition in validStartPositions)
        {
          workingSetOfStartingPositions.Push(validStartPosition);

          var newStartPosition = validStartPosition + runLength + 1;
          RecursivelyFindSetsOfStartingPositions(newStartPosition, newRemainingLengths);

          workingSetOfStartingPositions.Pop();
        }
      };

      _logger.LogInformation($"processing horizontal run group for row: {horizontalRunGroup.Row}");

      RecursivelyFindSetsOfStartingPositions(0, horizontalRunGroup.Lengths);

      _logger.LogInformation($"found the following starting positions for horizontal run group for row: {horizontalRunGroup.Row}");
      foreach (var setOfStartingPositions in setsOfStartingPositions)
      {
        _logger.LogInformation($"  {intsToString(setOfStartingPositions)}");
      }
    }

    return internalRows.ToArray();
  }

  public int[] InternalRowToMatrixRow(object internalRow)
  {
    var nonogramInternalRow = internalRow as NonogramInternalRow;
    return BuildMatrixRow(nonogramInternalRow);
  }

  public int? GetNumPrimaryColumns(object demoSettings)
  {
    var puzzle = (Puzzle)demoSettings;
    return puzzle.HorizontalRunGroups.Length + puzzle.VerticalRunGroups.Length;
  }

  public int ProgressFrequency { get => 10; }

  private int[] BuildMatrixRow(NonogramInternalRow internalRow)
  {
    var puzzle = internalRow.Puzzle;
    var runGroup = internalRow.RunGroup;
    var runCoordsLists = internalRow.RunCoordsLists;
    var rowColumns = MakeRowColumns(puzzle, runGroup);
    var colColumns = MakeColColumns(puzzle, runGroup);
    var horizontalCoordsColumns = MakeHorizontalCoordsColumns(puzzle, runGroup, runCoordsLists);
    var verticalCoordsColumns = MakeVerticalCoordsColumns(puzzle, runGroup, runCoordsLists);
    return rowColumns
      .Concat(colColumns)
      .Concat(horizontalCoordsColumns)
      .Concat(verticalCoordsColumns)
      .ToArray();
  }

  private int[] MakeRowColumns(Puzzle puzzle, RunGroup runGroup)
  {
    var columns = Enumerable.Repeat(0, puzzle.HorizontalRunGroups.Length).ToArray();
    if (runGroup.RunGroupType == RunGroupType.Horizontal)
    {
      var horizontalRunGroup = runGroup as HorizontalRunGroup;
      columns[horizontalRunGroup.Row] = 1;
    }
    return columns;
  }

  private int[] MakeColColumns(Puzzle puzzle, RunGroup runGroup)
  {
    var columns = Enumerable.Repeat(0, puzzle.VerticalRunGroups.Length).ToArray();
    if (runGroup.RunGroupType == RunGroupType.Vertical)
    {
      var verticalRunGroup = runGroup as VerticalRunGroup;
      columns[verticalRunGroup.Col] = 1;
    }
    return columns;
  }

  private int[] MakeHorizontalCoordsColumns(Puzzle puzzle, RunGroup runGroup, RunCoordsList[] runCoordsLists)
  {
    var size = puzzle.Size;
    var columns = Enumerable.Repeat(0, size * size * 2).ToArray();

    var encodedValue = runGroup.RunGroupType == RunGroupType.Horizontal
      ? EncodeValueNormal()
      : EncodeValueInverse();

    foreach (var runCoordsList in runCoordsLists)
    {
      foreach (var coords in runCoordsList.CoordsList)
      {
        var baseIndex = (coords.Row * size + coords.Col) * 2;
        foreach (var index in Enumerable.Range(0, encodedValue.Length))
        {
          columns[baseIndex + index] = encodedValue[index];
        }
      }
    }

    return columns;
  }

  private int[] MakeVerticalCoordsColumns(Puzzle puzzle, RunGroup runGroup, RunCoordsList[] runCoordsLists)
  {
    var size = puzzle.Size;
    var columns = Enumerable.Repeat(0, size * size * 2).ToArray();

    var encodedValue = runGroup.RunGroupType == RunGroupType.Vertical
      ? EncodeValueNormal()
      : EncodeValueInverse();

    foreach (var runCoordsList in runCoordsLists)
    {
      foreach (var coords in runCoordsList.CoordsList)
      {
        var baseIndex = (coords.Row * size + coords.Col) * 2;
        foreach (var index in Enumerable.Range(0, encodedValue.Length))
        {
          columns[baseIndex + index] = encodedValue[index];
        }
      }
    }

    return columns;
  }

  private int[] EncodeValueNormal()
  {
    return new[] { 1, 0 };
  }

  private int[] EncodeValueInverse()
  {
    return new[] { 0, 1 };
  }
}
