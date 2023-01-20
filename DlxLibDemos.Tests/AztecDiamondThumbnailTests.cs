using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.AztecDiamond;

namespace DlxLibDemos.Tests;

public class AztecDiamondThumbnailTests
{
  [Fact]
  public void ThumbnailSolutionIsSameAsDemoSolution()
  {
    var thumbnail = new AztecDiamondThumbnailWhatToDraw();
    var solutionInternalRows1 = thumbnail.SolutionInternalRows;

    var mockLogger = new NullLogger<AztecDiamondDemo>();
    var demo = new AztecDiamondDemo(mockLogger);
    var solutionInternalRows2 = Helpers.FindFirstSolution(demo);

    var internalRows1 = solutionInternalRows1
      .Cast<AztecDiamondInternalRow>()
      .OrderBy(internalRow => internalRow.Label);

    var internalRows2 = solutionInternalRows2
      .Cast<AztecDiamondInternalRow>()
      .OrderBy(internalRow => internalRow.Label);

    Assert.Equal(internalRows1, internalRows2);
  }
}
