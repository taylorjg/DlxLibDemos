using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.TetraSticks;

namespace DlxLibDemos.Tests;

public class TetraSticksThumbnailTests
{
  [Fact]
  public void ThumbnailSolutionIsSameAsDemoSolution()
  {
    var thumbnail = new TetraSticksThumbnailWhatToDraw();
    var solutionInternalRows1 = thumbnail.SolutionInternalRows;

    var mockLogger = new NullLogger<TetraSticksDemo>();
    var demo = new TetraSticksDemo(mockLogger);
    var solutionInternalRows2 = Helpers.FindFirstSolution(demo, thumbnail.DemoSettings);

    Assert.Equal(solutionInternalRows1, solutionInternalRows2);
  }
}
