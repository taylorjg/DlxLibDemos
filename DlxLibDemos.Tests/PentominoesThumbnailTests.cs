using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.Pentominoes;

namespace DlxLibDemos.Tests;

public class PentominoesThumbnailTests
{
  [Fact]
  public void ThumbnailSolutionIsSameAsDemoSolution()
  {
    var thumbnail = new PentominoesThumbnailWhatToDraw();
    var solutionInternalRows1 = thumbnail.SolutionInternalRows;

    var mockLogger = new NullLogger<PentominoesDemo>();
    var demo = new PentominoesDemo(mockLogger);
    var solutionInternalRows2 = Helpers.FindFirstSolution(demo);

    Assert.Equal(solutionInternalRows1, solutionInternalRows2);
  }
}
