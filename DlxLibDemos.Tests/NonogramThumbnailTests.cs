using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.Nonogram;
using System.Diagnostics.CodeAnalysis;

namespace DlxLibDemos.Tests;

public class NonogramThumbnailTests
{
  [Fact]
  public void ThumbnailSolutionIsSameAsDemoSolution()
  {
    var thumbnail = new NonogramThumbnailWhatToDraw();
    var solutionInternalRows1 = thumbnail.SolutionInternalRows;

    var mockLogger = new NullLogger<NonogramDemo>();
    var demo = new NonogramDemo(mockLogger);
    var solutionInternalRows2 = Helpers.FindFirstSolution(demo, thumbnail.DemoSettings);

    var internalRows1 = solutionInternalRows1.Cast<NonogramInternalRow>().ToArray();
    var internalRows2 = solutionInternalRows2.Cast<NonogramInternalRow>().ToArray();

    var comparer = new NonogramInternalRowComparer();
    Assert.Equal(internalRows1, internalRows2, comparer);
  }

  private class NonogramInternalRowComparer : IEqualityComparer<NonogramInternalRow>
  {
    public bool Equals(NonogramInternalRow x, NonogramInternalRow y)
    {
      var flatCoordsList1 = x.RunCoordsLists.SelectMany(rcl => rcl.CoordsList);
      var flatCoordsList2 = y.RunCoordsLists.SelectMany(rcl => rcl.CoordsList);

      return (
        x.Puzzle == y.Puzzle &&
        x.RunGroup == y.RunGroup &&
        flatCoordsList1.SequenceEqual(flatCoordsList2)
      );
    }

    public int GetHashCode([DisallowNull] NonogramInternalRow obj)
    {
      throw new NotImplementedException();
    }
  }
}
