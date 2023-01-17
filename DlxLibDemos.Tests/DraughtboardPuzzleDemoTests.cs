using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.DraughtboardPuzzle;

namespace DlxLibDemos.Tests;

public class DraughtboardPuzzleDemoTests
{
  [Fact]
  public void CanFindValidSolution()
  {
    var mockLogger = new NullLogger<DraughtboardPuzzleDemo>();
    var demo = new DraughtboardPuzzleDemo(mockLogger);

    var solutionInternalRows = Helpers.FindFirstSolution(demo);

    CheckSolution(solutionInternalRows.Cast<DraughtboardPuzzleInternalRow>().ToArray());
  }

  private static void CheckSolution(DraughtboardPuzzleInternalRow[] internalRows)
  {
    Assert.Equal(14, internalRows.Length);

    var allSquares = internalRows.SelectMany(internalRow =>
      internalRow.Variation.Squares.Select(square =>
        internalRow.Location.Add(square.Coords)));

    Assert.Equal(64, allSquares.Distinct().Count());
  }
}
