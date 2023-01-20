using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.AztecDiamond;

namespace DlxLibDemos.Tests;

public class AztecDiamondDemoTests
{
  [Fact]
  public void CanFindValidSolution()
  {
    var mockLogger = new NullLogger<AztecDiamondDemo>();
    var demo = new AztecDiamondDemo(mockLogger);

    var solutionInternalRows = Helpers.FindFirstSolution(demo);

    CheckSolution(solutionInternalRows.Cast<AztecDiamondInternalRow>().ToArray());
  }

  private static void CheckSolution(AztecDiamondInternalRow[] internalRows)
  {
    Assert.Equal(25, internalRows.Length);

    CheckAllHorizontalsCovered(internalRows);
    CheckAllVertialsCovered(internalRows);
    CheckNoOverlappingJunctions(internalRows);
  }

  private static void CheckAllHorizontalsCovered(AztecDiamondInternalRow[] internalRows)
  {
    var allHorizontals = internalRows.SelectMany(internalRow =>
      internalRow.Variation.Horizontals.Select(coords =>
        internalRow.Location.Add(coords)));

    Assert.Equal(50, allHorizontals.Distinct().Count());
  }

  private static void CheckAllVertialsCovered(AztecDiamondInternalRow[] internalRows)
  {
    var allVerticals = internalRows.SelectMany(internalRow =>
      internalRow.Variation.Verticals.Select(coords =>
        internalRow.Location.Add(coords)));

    Assert.Equal(50, allVerticals.Distinct().Count());
  }

  private static void CheckNoOverlappingJunctions(AztecDiamondInternalRow[] internalRows)
  {
    var allJunctions = internalRows.SelectMany(internalRow =>
      internalRow.Variation.Junctions.Select(coords =>
        internalRow.Location.Add(coords)));

    Assert.Equal(allJunctions.Count(), allJunctions.Distinct().Count());
  }
}
