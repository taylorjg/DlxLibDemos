using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.Nonogram;

namespace DlxLibDemos.Tests;

public class NonogramDemoTests
{
  [Fact]
  public void CanFindValidSolution()
  {
    var mockLogger = new NullLogger<NonogramDemo>();
    var demo = new NonogramDemo(mockLogger);

    var puzzle = Puzzles.ThePuzzles.First();
    var demoSettings = puzzle;
    var solutionInternalRows = Helpers.FindFirstSolution(demo, demoSettings);

    CheckSolution(puzzle, solutionInternalRows.Cast<NonogramInternalRow>().ToArray());
  }

  private static void CheckSolution(Puzzle puzzle, NonogramInternalRow[] internalRows)
  {
    Assert.Equal(puzzle.HorizontalRunGroups.Length + puzzle.VerticalRunGroups.Length, internalRows.Length);
    CheckAllRunGroupsArePresent(puzzle, internalRows);
    CheckVerticalRunGroupsAreCorrect(puzzle, internalRows);
  }

  private static void CheckAllRunGroupsArePresent(Puzzle puzzle, NonogramInternalRow[] internalRows)
  {
    foreach (var runGroup in puzzle.HorizontalRunGroups)
    {
      var internalRow = internalRows.FirstOrDefault(internalRow => internalRow.RunGroup == runGroup);
      Assert.NotNull(internalRow);
    }

    foreach (var runGroup in puzzle.VerticalRunGroups)
    {
      var internalRow = internalRows.FirstOrDefault(internalRow => internalRow.RunGroup == runGroup);
      Assert.NotNull(internalRow);
    }
  }

  private static void CheckVerticalRunGroupsAreCorrect(Puzzle puzzle, NonogramInternalRow[] internalRows)
  {
    int[] RowsToRunGroupLengths(int[] rows)
    {
      var runGroupLengths = new List<int>();
      var currentRun = new List<int>();

      foreach (var row in rows)
      {
        if (!currentRun.Any()) currentRun.Add(row);
        else
        {
          if (row == currentRun.Last() + 1) currentRun.Add(row);
          else
          {
            runGroupLengths.Add(currentRun.Count());
            currentRun.Clear();
            currentRun.Add(row);
          }
        }
      }

      if (currentRun.Any()) runGroupLengths.Add(currentRun.Count());

      return runGroupLengths.ToArray();
    }

    int[] RebuildVerticalRunGroupLengths(int col)
    {
      var horizontalInternalRows = internalRows.Where(internalRow =>
        internalRow.RunGroup.RunGroupType == RunGroupType.Horizontal);

      var rows = horizontalInternalRows
        .SelectMany(horizontalInternalRow =>
          horizontalInternalRow.RunCoordsLists.SelectMany(runCoordsList =>
            runCoordsList.CoordsList.Where(coords =>
              coords.Col == col)))
        .Select(coords => coords.Row)
        .ToArray();

      return RowsToRunGroupLengths(rows);
    }

    foreach (var runGroup in puzzle.VerticalRunGroups)
    {
      var verticalRunGroup = runGroup as VerticalRunGroup;
      var runGroupLengths = RebuildVerticalRunGroupLengths(verticalRunGroup.Col);
      Assert.Equal(verticalRunGroup.Lengths, runGroupLengths);
    }
  }
}
