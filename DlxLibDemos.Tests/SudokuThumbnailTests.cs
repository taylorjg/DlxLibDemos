using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.Sudoku;

namespace DlxLibDemos.Tests;

public class SudokuThumbnailTests
{
  [Fact]
  public void ThumbnailSolutionIsSameAsDemoSolution()
  {
    var thumbnail = new SudokuThumbnailWhatToDraw();
    var solutionInternalRows1 = thumbnail.SolutionInternalRows;

    var mockLogger = new NullLogger<SudokuDemo>();
    var demo = new SudokuDemo(mockLogger);
    var solutionInternalRows2 = Helpers.FindFirstSolution(demo, thumbnail.DemoSettings);

    Assert.Equal(solutionInternalRows1, solutionInternalRows2);
  }
}
