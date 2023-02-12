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

  private record StartingPositionData(int StartingPosition, int RunLength);

  public object[] BuildInternalRows(object demoSettings, CancellationToken cancellationToken)
  {
    var puzzle = (Puzzle)demoSettings;
    var size = puzzle.Size;
    var internalRows = new List<NonogramInternalRow>();

    void BuildInternalRowsForRunGroup(RunGroup runGroup)
    {
      var workingSetOfStartingPositions = new Stack<StartingPositionData>();

      void RecursivelyFindSetsOfStartingPositions(int startPosition, int[] remainingLengths)
      {
        if (remainingLengths.Length == 0)
        {
          if (workingSetOfStartingPositions.Count == runGroup.Lengths.Length)
          {
            var setOfStartingPositions = workingSetOfStartingPositions.Reverse().ToArray();
            var runCoordsLists = new List<RunCoordsList>();
            foreach (var startingPositionData in setOfStartingPositions)
            {
              var coordsList = new List<Coords>();
              foreach (var startingPosition in Enumerable.Range(startingPositionData.StartingPosition, startingPositionData.RunLength))
              {
                var coords = runGroup.MakeCoords(startingPosition);
                coordsList.Add(coords);
              }
              var runCoordsList = new RunCoordsList(coordsList.ToArray());
              runCoordsLists.Add(runCoordsList);
            }
            var internalRow = new NonogramInternalRow(puzzle, runGroup, runCoordsLists.ToArray());
            internalRows.Add(internalRow);
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
          var pair = new StartingPositionData(validStartPosition, runLength);
          workingSetOfStartingPositions.Push(pair);

          var newStartPosition = validStartPosition + runLength + 1;
          RecursivelyFindSetsOfStartingPositions(newStartPosition, newRemainingLengths);

          workingSetOfStartingPositions.Pop();
        }
      };

      RecursivelyFindSetsOfStartingPositions(0, runGroup.Lengths);
    }

    foreach (var horizontalRunGroup in puzzle.HorizontalRunGroups)
    {
      BuildInternalRowsForRunGroup(horizontalRunGroup);
    }

    foreach (var verticalRunGroup in puzzle.VerticalRunGroups)
    {
      BuildInternalRowsForRunGroup(verticalRunGroup);
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

  public int ProgressFrequency { get => 100; }

  private int[] BuildMatrixRow(NonogramInternalRow internalRow)
  {
    var puzzle = internalRow.Puzzle;
    var runGroup = internalRow.RunGroup;
    var runCoordsLists = internalRow.RunCoordsLists;
    var rowColumns = MakeRowColumns(puzzle, runGroup);
    var colColumns = MakeColColumns(puzzle, runGroup);
    var horizontalBlockColumns = MakeBlockColumns(puzzle, runGroup, runCoordsLists, RunGroupType.Horizontal);
    var verticalBlockColumns = MakeBlockColumns(puzzle, runGroup, runCoordsLists, RunGroupType.Vertical);
    return rowColumns
      .Concat(colColumns)
      .Concat(horizontalBlockColumns)
      .Concat(verticalBlockColumns)
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

  private int[] MakeBlockColumns(Puzzle puzzle, RunGroup runGroup, RunCoordsList[] runCoordsLists, RunGroupType runGroupType)
  {
    var size = puzzle.Size;
    var columns = Enumerable.Repeat(0, size * size * 2).ToArray();

    var selectedBlockCoords = runCoordsLists.SelectMany(runCoordsList => runCoordsList.CoordsList).ToArray();

    if (runGroup.RunGroupType == runGroupType)
    {
      var allBlockCoords = Enumerable.Range(0, size).Select(runGroup.MakeCoords).ToArray();
      var unselectedBlockCoords = allBlockCoords.Except(selectedBlockCoords);
      foreach (var coords in selectedBlockCoords) MarkOn(columns, size, coords);
      foreach (var coords in unselectedBlockCoords) MarkOff(columns, size, coords);
    }
    else
    {
      foreach (var coords in selectedBlockCoords) MarkOff(columns, size, coords);
    }

    return columns;
  }

  const int ON_INDEX = 0;
  const int OFF_INDEX = 1;

  private void MarkOn(int[] columns, int size, Coords coords)
  {
    var baseIndex = (coords.Row * size + coords.Col) * 2;
    columns[baseIndex + ON_INDEX] = 1;
  }

  private void MarkOff(int[] columns, int size, Coords coords)
  {
    var baseIndex = (coords.Row * size + coords.Col) * 2;
    columns[baseIndex + OFF_INDEX] = 1;
  }
}
