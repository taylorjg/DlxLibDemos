using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.RippleEffect;
using System.Diagnostics.CodeAnalysis;

namespace DlxLibDemos.Tests;

public class RippleEffectThumbnailTests
{
  [Fact]
  public void ThumbnailSolutionIsSameAsDemoSolution()
  {
    var thumbnail = new RippleEffectThumbnailWhatToDraw();
    var solutionInternalRows1 = thumbnail.SolutionInternalRows;

    var mockLogger = new NullLogger<RippleEffectDemo>();
    var demo = new RippleEffectDemo(mockLogger);
    var solutionInternalRows2 = Helpers.FindFirstSolution(demo, thumbnail.DemoSettings);

    var internalRows1 = solutionInternalRows1
      .Cast<RippleEffectInternalRow>()
      .OrderBy(ir => ir.Room.StartIndex)
      .OrderBy(ir => ir.Value)
      .ToArray();

    var internalRows2 = solutionInternalRows2
      .Cast<RippleEffectInternalRow>()
      .OrderBy(ir => ir.Room.StartIndex)
      .OrderBy(ir => ir.Value)
      .ToArray();

    Assert.Equal(internalRows1, internalRows2);
  }
}
