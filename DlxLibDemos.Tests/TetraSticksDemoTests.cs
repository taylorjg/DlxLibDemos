using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.TetraSticks;

namespace DlxLibDemos.Tests;

public class TetraSticksDemoTests
{
  [Fact]
  public void CanFindValidSolution()
  {
    var mockLogger = new NullLogger<TetraSticksDemo>();
    var demo = new TetraSticksDemo(mockLogger);

    var missingLetter = TetraSticksDemoPageViewModel.TheMissingLetters.First();
    var demoSettings = missingLetter;
    var solutionInternalRows = Helpers.FindFirstSolution(demo, demoSettings);

    CheckSolution(solutionInternalRows.Cast<TetraSticksInternalRow>().ToArray());
  }

  private static void CheckSolution(TetraSticksInternalRow[] internalRows)
  {
    Assert.Equal(15, internalRows.Length);

    CheckAllHorizontalsCovered(internalRows);
    CheckAllVertialsCovered(internalRows);
    CheckNoOverlappingJunctions(internalRows);
  }

  private static void CheckAllHorizontalsCovered(TetraSticksInternalRow[] internalRows)
  {
    var allHorizontals = internalRows.SelectMany(internalRow =>
      internalRow.Variation.Horizontals.Select(coords =>
        internalRow.Location.Add(coords)));

    Assert.Equal(30, allHorizontals.Distinct().Count());
  }

  private static void CheckAllVertialsCovered(TetraSticksInternalRow[] internalRows)
  {
    var allVerticals = internalRows.SelectMany(internalRow =>
      internalRow.Variation.Verticals.Select(coords =>
        internalRow.Location.Add(coords)));

    Assert.Equal(30, allVerticals.Distinct().Count());
  }

  private static void CheckNoOverlappingJunctions(TetraSticksInternalRow[] internalRows)
  {
    var allJunctions = internalRows.SelectMany(internalRow =>
      internalRow.Variation.Junctions.Select(coords =>
        internalRow.Location.Add(coords)));

    Assert.Equal(allJunctions.Count(), allJunctions.Distinct().Count());
  }
}
