using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.Crossword;

namespace DlxLibDemos.Tests;

public class CrosswordThumbnailTests
{
  [Fact]
  public void ThumbnailSolutionIsSameAsDemoSolution()
  {
    var thumbnail = new CrosswordStaticThumbnailWhatToDraw();
    var solutionInternalRows1 = thumbnail.SolutionInternalRows;

    var mockLogger = new NullLogger<CrosswordDemo>();
    var demo = new CrosswordDemo(mockLogger);
    var solutionInternalRows2 = Helpers.FindFirstSolution(demo, thumbnail.DemoSettings);

    Assert.Equal(solutionInternalRows1, solutionInternalRows2);
  }
}
