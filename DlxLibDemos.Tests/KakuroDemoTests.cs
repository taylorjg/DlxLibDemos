using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.Kakuro;

namespace DlxLibDemos.Tests;

public class KakuroDemoTests
{
  [Fact]
  public void CanFindValidSolution()
  {
    var mockLogger = new NullLogger<KakuroDemo>();
    var demo = new KakuroDemo(mockLogger);

    var solutionInternalRows = Helpers.FindFirstSolution(demo);

    var puzzle = Puzzles.ThePuzzles.First();
    CheckSolution(puzzle, solutionInternalRows.Cast<KakuroInternalRow>().ToArray());
  }

  private static void CheckSolution(Puzzle puzzle, KakuroInternalRow[] internalRows)
  {
    Assert.Equal(puzzle.HorizontalRuns.Length + puzzle.VerticalRuns.Length, internalRows.Length);
    CheckDigits(internalRows);
    CheckRuns(puzzle, internalRows);
    CheckRunSums(internalRows);
  }

  private static void CheckDigits(KakuroInternalRow[] internalRows)
  {
    foreach (var internalRow in internalRows)
    {
      foreach (var value in internalRow.Values)
      {
        Assert.InRange(value, 1, 9);
      }
    }
  }

  private static void CheckRuns(Puzzle puzzle, KakuroInternalRow[] internalRows)
  {
    foreach (var run in puzzle.HorizontalRuns)
    {
      var internalRow = internalRows.FirstOrDefault(internalRow => internalRow.Run == run);
      Assert.NotNull(internalRow);
    }

    foreach (var run in puzzle.VerticalRuns)
    {
      var internalRow = internalRows.FirstOrDefault(internalRow => internalRow.Run == run);
      Assert.NotNull(internalRow);
    }
  }

  private static void CheckRunSums(KakuroInternalRow[] internalRows)
  {
    foreach (var internalRow in internalRows)
    {
      Assert.Equal(internalRow.Run.Sum, internalRow.Values.Sum());
    }
  }
}
