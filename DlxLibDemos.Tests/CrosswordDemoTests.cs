using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.Crossword;

namespace DlxLibDemos.Tests;

public class CrosswordDemoTests
{
  [Fact]
  public void CanFindValidSolution()
  {
    var mockLogger = new NullLogger<CrosswordDemo>();
    var demo = new CrosswordDemo(mockLogger);

    var solutionInternalRows = Helpers.FindFirstSolution(demo);

    var puzzle = Puzzles.ThePuzzles.First();
    CheckSolution(puzzle, solutionInternalRows.Cast<CrosswordInternalRow>().ToArray());
  }

  private static void CheckSolution(Puzzle puzzle, CrosswordInternalRow[] internalRows)
  {
    var puzzleClueNumbers = puzzle.Clues.Select(clue => clue.ClueNumber);
    var solutionClueNumbers = internalRows.Select(internalRow => internalRow.Clue.ClueNumber);
    Assert.Equal(puzzleClueNumbers, solutionClueNumbers);

    CheckAnswers(puzzle, internalRows);
  }

  private static void CheckAnswers(Puzzle puzzle, CrosswordInternalRow[] internalRows)
  {
    foreach (var internalRow in internalRows)
    {
      Assert.Equal(internalRow.Clue.Candidates.First(), internalRow.Candidate);
    }
  }
}
