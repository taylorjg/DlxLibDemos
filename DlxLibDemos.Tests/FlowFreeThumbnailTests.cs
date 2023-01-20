using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.FlowFree;
using System.Diagnostics.CodeAnalysis;

namespace DlxLibDemos.Tests;

public class FlowFreeThumbnailTests
{
  [Fact]
  public void ThumbnailHardcodedSolutionIsSameAsDemoCalculatedSolution()
  {
    var thumbnail = new FlowFreeStaticThumbnailWhatToDraw();
    var solutionInternalRows1 = thumbnail.SolutionInternalRows;

    var mockLogger = new NullLogger<FlowFreeDemo>();
    var demo = new FlowFreeDemo(mockLogger);
    var solutionInternalRows2 = Helpers.FindFirstSolution(demo, thumbnail.DemoSettings);

    var internalRows1 = solutionInternalRows1
      .Cast<FlowFreeInternalRow>()
      .OrderBy(internalRow => internalRow.ColourPair.Label);

    var internalRows2 = solutionInternalRows2
      .Cast<FlowFreeInternalRow>()
      .OrderBy(internalRow => internalRow.ColourPair.Label);

    var comparer = new FlowFreeInternalRowComparer();
    Assert.Equal(internalRows1, internalRows2, comparer);
  }

  private class FlowFreeInternalRowComparer : IEqualityComparer<FlowFreeInternalRow>
  {
    public bool Equals(FlowFreeInternalRow x, FlowFreeInternalRow y)
    {
      return (
        x.Puzzle == y.Puzzle &&
        x.ColourPair == y.ColourPair &&
        x.Pipe.Reverse().SequenceEqual(y.Pipe)
      );
    }

    public int GetHashCode([DisallowNull] FlowFreeInternalRow obj)
    {
      throw new NotImplementedException();
    }
  }
}
