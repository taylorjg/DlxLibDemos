using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.Pentominoes;

namespace DlxLibDemos.Tests;

public class PentominoesDemoTests
{
  [Fact]
  public void CanFindValidSolution()
  {
    var mockLogger = new NullLogger<PentominoesDemo>();
    var demo = new PentominoesDemo(mockLogger);

    var solutionInternalRows = Helpers.FindFirstSolution(demo);

    CheckSolution(solutionInternalRows.Cast<PentominoesInternalRow>().ToArray());
  }

  private static void CheckSolution(PentominoesInternalRow[] internalRows)
  {
    Assert.Equal(12, internalRows.Length);

    var allSquares = internalRows.SelectMany(internalRow =>
      internalRow.Variation.CoordsList.Select(coords =>
        internalRow.Location.Add(coords)));

    Assert.Equal(60, allSquares.Distinct().Count());

    var reservedSquares = new[] {
      new Coords(3, 3),
      new Coords(3, 4),
      new Coords(4, 3),
      new Coords(4, 4)
    };

    Assert.Equal(64, allSquares.Concat(reservedSquares).Distinct().Count());
  }
}
