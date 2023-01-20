using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.NQueens;

namespace DlxLibDemos.Tests;

public class NQueensThumbnailTests
{
  [Fact]
  public void ThumbnailSolutionIsSameAsDemoSolution()
  {
    var thumbnail = new NQueensThumbnailWhatToDraw();
    var solutionInternalRows1 = thumbnail.SolutionInternalRows;

    var mockLogger = new NullLogger<NQueensDemo>();
    var demo = new NQueensDemo(mockLogger);
    var solutionInternalRows2 = Helpers.FindFirstSolution(demo, thumbnail.DemoSettings);

    Assert.Equal(solutionInternalRows1, solutionInternalRows2);
  }
}
