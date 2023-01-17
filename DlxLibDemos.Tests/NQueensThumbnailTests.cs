using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.NQueens;

namespace DlxLibDemos.Tests;

public class NQueensThumbnailTests
{
  [Fact]
  public void ThumbnailHardcodedSolutionIsSameAsDemoCalculatedSolution()
  {
    var thumbnail = new NQueensStaticThumbnailWhatToDraw();
    var solutionInternalRows1 = thumbnail.SolutionInternalRows;

    var mockLogger = new NullLogger<NQueensDemo>();
    var demo = new NQueensDemo(mockLogger);
    var solutionInternalRows2 = Helpers.FindFirstSolution(demo, thumbnail.DemoSettings);

    var size = (int)thumbnail.DemoSettings;
    Assert.Equal(size, solutionInternalRows1.Length);
    Assert.Equal(size, solutionInternalRows2.Length);

    foreach (var index in Enumerable.Range(0, size))
    {
      var solutionInternalRow1 = solutionInternalRows1[index];
      var solutionInternalRow2 = solutionInternalRows2[index];
      Assert.Equal(solutionInternalRow1, solutionInternalRow2);
    }
  }
}
