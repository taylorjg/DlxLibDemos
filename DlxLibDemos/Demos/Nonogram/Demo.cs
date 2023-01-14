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
    var pairsToString = (IEnumerable<(int, int)> pairs) => "[" + string.Join(", ", pairs.Select(pair => $"{pair.Item1}/{pair.Item2}")) + "]";

    var size = puzzle.Size;

    foreach (var horizontalRunGroup in puzzle.HorizontalRunGroups)
    {
      var setsOfStartingPositions = new List<(int, int)[]>();
      var workingSetOfStartingPositions = new Stack<(int, int)>();

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
          workingSetOfStartingPositions.Push((validStartPosition, runLength));

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
        _logger.LogInformation($"  {pairsToString(setOfStartingPositions)}");
        var runCoordsLists = new List<RunCoordsList>();
        foreach (var pair in setOfStartingPositions)
        {
          var startingPosition = pair.Item1;
          var runLength = pair.Item2;
          var coordsList = new List<Coords>();
          foreach (var col in Enumerable.Range(startingPosition, runLength))
          {
            var coords = new Coords(horizontalRunGroup.Row, col);
            coordsList.Add(coords);
          }
          var runCoordsList = new RunCoordsList(coordsList.ToArray());
          runCoordsLists.Add(runCoordsList);
        }
        var internalRow = new NonogramInternalRow(puzzle, horizontalRunGroup, runCoordsLists.ToArray());
        internalRows.Add(internalRow);
      }
    }

    foreach (var verticalRunGroup in puzzle.VerticalRunGroups)
    {
      var setsOfStartingPositions = new List<(int, int)[]>();
      var workingSetOfStartingPositions = new Stack<(int, int)>();

      void RecursivelyFindSetsOfStartingPositions(int startPosition, int[] remainingLengths)
      {
        if (remainingLengths.Length == 0)
        {
          if (workingSetOfStartingPositions.Count == verticalRunGroup.Lengths.Length)
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
          workingSetOfStartingPositions.Push((validStartPosition, runLength));

          var newStartPosition = validStartPosition + runLength + 1;
          RecursivelyFindSetsOfStartingPositions(newStartPosition, newRemainingLengths);

          workingSetOfStartingPositions.Pop();
        }
      };

      _logger.LogInformation($"processing vertical run group for col: {verticalRunGroup.Col}");

      RecursivelyFindSetsOfStartingPositions(0, verticalRunGroup.Lengths);

      _logger.LogInformation($"found the following starting positions for vertical run group for col: {verticalRunGroup.Col}");
      foreach (var setOfStartingPositions in setsOfStartingPositions)
      {
        _logger.LogInformation($"  {pairsToString(setOfStartingPositions)}");
        var runCoordsLists = new List<RunCoordsList>();
        foreach (var pair in setOfStartingPositions)
        {
          var startingPosition = pair.Item1;
          var runLength = pair.Item2;
          var coordsList = new List<Coords>();
          foreach (var row in Enumerable.Range(startingPosition, runLength))
          {
            var coords = new Coords(row, verticalRunGroup.Col);
            coordsList.Add(coords);
          }
          var runCoordsList = new RunCoordsList(coordsList.ToArray());
          runCoordsLists.Add(runCoordsList);
        }
        var internalRow = new NonogramInternalRow(puzzle, verticalRunGroup, runCoordsLists.ToArray());
        internalRows.Add(internalRow);
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
    var puzzle = Puzzles.ThePuzzles.First();
    return puzzle.HorizontalRunGroups.Length + puzzle.VerticalRunGroups.Length;
  }

  public int ProgressFrequency { get => 10000; }

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

  const int ON_INDEX = 0;
  const int OFF_INDEX = 1;

  private int[] MakeHorizontalCoordsColumns(Puzzle puzzle, RunGroup runGroup, RunCoordsList[] runCoordsLists)
  {
    var size = puzzle.Size;
    var columns = Enumerable.Repeat(0, size * size * 2).ToArray();

    var selectedRowCoords = runCoordsLists.SelectMany(runCoordsList => runCoordsList.CoordsList).ToArray();

    if (runGroup.RunGroupType == RunGroupType.Horizontal)
    {
      var row = (runGroup as HorizontalRunGroup).Row;
      var allRowCoords = Enumerable.Range(0, size).Select(col => new Coords(row, col)).ToArray();
      var unselectedRowCoords = allRowCoords.Except(selectedRowCoords);
      foreach (var coords in selectedRowCoords)
      {
        var baseIndex = (coords.Row * size + coords.Col) * 2;
        columns[baseIndex + ON_INDEX] = 1;
      }
      foreach (var coords in unselectedRowCoords)
      {
        var baseIndex = (coords.Row * size + coords.Col) * 2;
        columns[baseIndex + OFF_INDEX] = 1;
      }
    }
    else
    {
      foreach (var coords in selectedRowCoords)
      {
        var baseIndex = (coords.Row * size + coords.Col) * 2;
        columns[baseIndex + OFF_INDEX] = 1;
      }
    }

    return columns;
  }

  private int[] MakeVerticalCoordsColumns(Puzzle puzzle, RunGroup runGroup, RunCoordsList[] runCoordsLists)
  {
    var size = puzzle.Size;
    var columns = Enumerable.Repeat(0, size * size * 2).ToArray();

    var selectedColCoords = runCoordsLists.SelectMany(runCoordsList => runCoordsList.CoordsList).ToArray();

    if (runGroup.RunGroupType == RunGroupType.Vertical)
    {
      var col = (runGroup as VerticalRunGroup).Col;
      var allColCoords = Enumerable.Range(0, size).Select(row => new Coords(row, col)).ToArray();
      var unselectedColCoords = allColCoords.Except(selectedColCoords);
      foreach (var coords in selectedColCoords)
      {
        var baseIndex = (coords.Row * size + coords.Col) * 2;
        columns[baseIndex + ON_INDEX] = 1;
      }
      foreach (var coords in unselectedColCoords)
      {
        var baseIndex = (coords.Row * size + coords.Col) * 2;
        columns[baseIndex + OFF_INDEX] = 1;
      }
    }
    else
    {
      foreach (var coords in selectedColCoords)
      {
        var baseIndex = (coords.Row * size + coords.Col) * 2;
        columns[baseIndex + OFF_INDEX] = 1;
      }
    }

    return columns;
  }
}
