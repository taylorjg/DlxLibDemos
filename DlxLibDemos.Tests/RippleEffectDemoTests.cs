using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.RippleEffect;

namespace DlxLibDemos.Tests;

public class RippleEffectDemoTests
{
  [Fact]
  public void CanFindValidSolution()
  {
    var mockLogger = new NullLogger<RippleEffectDemo>();
    var demo = new RippleEffectDemo(mockLogger);

    var puzzle = Puzzles.ThePuzzles.First();
    var demoSettings = puzzle;
    var solutionInternalRows = Helpers.FindFirstSolution(demo, demoSettings);

    CheckSolution(puzzle, solutionInternalRows.Cast<RippleEffectInternalRow>().ToArray());
  }

  private static void CheckSolution(Puzzle puzzle, RippleEffectInternalRow[] internalRows)
  {
    var size = puzzle.Size;
    Assert.Equal(size * size, internalRows.Length);
    CheckValueAdjacenies(puzzle, internalRows);
    CheckRoomValues(puzzle, internalRows);
  }

  private static void CheckValueAdjacenies(Puzzle puzzle, RippleEffectInternalRow[] internalRows)
  {
    var size = puzzle.Size;

    foreach (var row in Enumerable.Range(0, size))
    {
      foreach (var col in Enumerable.Range(0, size))
      {
      }
    }
  }

  private static void CheckRoomValues(Puzzle puzzle, RippleEffectInternalRow[] internalRows)
  {
    foreach (var room in puzzle.Rooms)
    {
    }
  }
}
