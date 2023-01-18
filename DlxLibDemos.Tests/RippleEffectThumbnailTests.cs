using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.RippleEffect;
using System.Diagnostics.CodeAnalysis;

namespace DlxLibDemos.Tests;

public class RippleEffectThumbnailTests
{
  [Fact]
  public void ThumbnailHardcodedSolutionIsSameAsDemoCalculatedSolution()
  {
    var thumbnail = new RippleEffectStaticThumbnailWhatToDraw();
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

    var comparer = new RippleEffectInternalRowComparer();
    Assert.Equal(internalRows1, internalRows2, comparer);
  }

  private class RippleEffectInternalRowComparer : IEqualityComparer<RippleEffectInternalRow>
  {
    public bool Equals(RippleEffectInternalRow x, RippleEffectInternalRow y)
    {
      return (
        x.Cell == y.Cell &&
        x.IsInitialValue == y.IsInitialValue &&
        x.Value == y.Value &&
        x.Room == y.Room
      );
    }

    public int GetHashCode([DisallowNull] RippleEffectInternalRow obj)
    {
      throw new NotImplementedException();
    }
  }
}
