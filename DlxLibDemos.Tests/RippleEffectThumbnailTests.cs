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

    var comparer = new RippleEffectInternalRowComparer();
    Assert.Equal(solutionInternalRows1, solutionInternalRows2, comparer);
  }

  private class RippleEffectInternalRowComparer : IEqualityComparer<object>
  {
    public new bool Equals(object x, object y)
    {
      var internalRow1 = x as RippleEffectInternalRow;
      var internalRow2 = x as RippleEffectInternalRow;

      return (
        internalRow1.Cell == internalRow2.Cell &&
        internalRow1.IsInitialValue == internalRow2.IsInitialValue &&
        internalRow1.Value == internalRow2.Value &&
        internalRow1.Room == internalRow2.Room
      );
    }

    public int GetHashCode([DisallowNull] object obj)
    {
      throw new NotImplementedException();
    }
  }
}
