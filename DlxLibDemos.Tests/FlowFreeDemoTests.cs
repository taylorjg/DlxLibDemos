using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.FlowFree;

namespace DlxLibDemos.Tests;

public class FlowFreeDemoTests
{
  [Fact]
  public void CanFindValidSolution()
  {
    var mockLogger = new NullLogger<FlowFreeDemo>();
    var demo = new FlowFreeDemo(mockLogger);

    var puzzle = Puzzles.ThePuzzles.First();
    var demoSettings = puzzle;
    var solutionInternalRows = Helpers.FindFirstSolution(demo, demoSettings);

    CheckSolution(puzzle, solutionInternalRows.Cast<FlowFreeInternalRow>().ToArray());
  }

  private static void CheckSolution(Puzzle puzzle, FlowFreeInternalRow[] internalRows)
  {
    Assert.Equal(puzzle.ColourPairs.Length, internalRows.Length);

    CheckAllSquaresCovered(puzzle, internalRows);
    CheckPipes(puzzle, internalRows);
  }

  private static void CheckAllSquaresCovered(Puzzle puzzle, FlowFreeInternalRow[] internalRows)
  {
    var allSquares = internalRows.SelectMany(internalRow => internalRow.Pipe);
    Assert.Equal(puzzle.Size * puzzle.Size, allSquares.Distinct().Count());
  }

  private static void CheckPipes(Puzzle puzzle, FlowFreeInternalRow[] internalRows)
  {
    foreach (var internalRow in internalRows)
    {
      CheckPipe(internalRow.Pipe);
    }
  }

  private static void CheckPipe(Coords[] pipe)
  {
    foreach (var index in Enumerable.Range(0, pipe.Length).Skip(1))
    {
      var currCoords = pipe[index];
      var prevCoords = pipe[index - 1];
      var rowDiff = Math.Abs(currCoords.Row - prevCoords.Row);
      var colDiff = Math.Abs(currCoords.Col - prevCoords.Col);
      var manhattanDistance = rowDiff + colDiff;
      Assert.Equal(1, manhattanDistance);
    }
  }
}
