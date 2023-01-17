using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.DraughtboardPuzzle;

namespace DlxLibDemos.Tests;

public class DraughtboardPuzzleThumbnailTests
{
  [Fact]
  public void ThumbnailHardcodedSolutionIsSameAsDemoCalculatedSolution()
  {
    var thumbnail = new DraughtboardPuzzleStaticThumbnailWhatToDraw();
    var solutionInternalRows1 = thumbnail.SolutionInternalRows;

    var mockLogger = new NullLogger<DraughtboardPuzzleDemo>();
    var demo = new DraughtboardPuzzleDemo(mockLogger);
    var solutionInternalRows2 = Helpers.FindFirstSolution(demo);

    Assert.Equal(solutionInternalRows1, solutionInternalRows2);
  }
}
