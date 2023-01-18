using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.Kakuro;
using System.Diagnostics.CodeAnalysis;

namespace DlxLibDemos.Tests;

public class KakuroThumbnailTests
{
  [Fact]
  public void ThumbnailHardcodedSolutionIsSameAsDemoCalculatedSolution()
  {
    var thumbnail = new KakuroStaticThumbnailWhatToDraw();
    var solutionInternalRows1 = thumbnail.SolutionInternalRows;

    var mockLogger = new NullLogger<KakuroDemo>();
    var demo = new KakuroDemo(mockLogger);
    var solutionInternalRows2 = Helpers.FindFirstSolution(demo, thumbnail.DemoSettings);

    var internalRows1 = solutionInternalRows1.Cast<KakuroInternalRow>().ToArray();
    var internalRows2 = solutionInternalRows2.Cast<KakuroInternalRow>().ToArray();

    var comparer = new KakuroInternalRowComparer();
    Assert.Equal(internalRows1, internalRows2, comparer);
  }

  private class KakuroInternalRowComparer : IEqualityComparer<KakuroInternalRow>
  {
    public bool Equals(KakuroInternalRow x, KakuroInternalRow y)
    {
      return (
        x.Run == y.Run &&
        x.Values.SequenceEqual(y.Values)
      );
    }

    public int GetHashCode([DisallowNull] KakuroInternalRow obj)
    {
      throw new NotImplementedException();
    }
  }
}
