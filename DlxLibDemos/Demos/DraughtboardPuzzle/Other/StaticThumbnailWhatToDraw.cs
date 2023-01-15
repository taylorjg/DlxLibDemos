namespace DlxLibDemos.Demos.DraughtboardPuzzle;

public class DraughtboardPuzzleStaticThumbnailWhatToDraw : IWhatToDraw
{
  public object DemoSettings { get; private init; }
  public object DemoDrawingOptions { get; private init; }
  public object[] SolutionInternalRows { get; private init; }

  public DraughtboardPuzzleStaticThumbnailWhatToDraw()
  {
    DemoDrawingOptions = false;
    SolutionInternalRows = MakeSolution();
  }

  private static DraughtboardPuzzleInternalRow[] MakeSolution()
  {
    return new[]
    {
      MakeSolutionInternalRow("A", Orientation.North, 2, 0),
      MakeSolutionInternalRow("B", Orientation.South, 1, 4),
      MakeSolutionInternalRow("C", Orientation.West, 6, 6),
      MakeSolutionInternalRow("D", Orientation.North, 5, 1),
      MakeSolutionInternalRow("E", Orientation.East, 3, 1),
      MakeSolutionInternalRow("F", Orientation.East, 0, 1),
      MakeSolutionInternalRow("G", Orientation.West, 5, 0),
      MakeSolutionInternalRow("H", Orientation.North, 3, 5),
      MakeSolutionInternalRow("I", Orientation.South, 2, 6),
      MakeSolutionInternalRow("J", Orientation.West, 0, 4),
      MakeSolutionInternalRow("K", Orientation.North, 5, 4),
      MakeSolutionInternalRow("L", Orientation.East, 0, 0),
      MakeSolutionInternalRow("M", Orientation.North, 4, 3),
      MakeSolutionInternalRow("N", Orientation.East, 2, 2)
    };
  }

  private static DraughtboardPuzzleInternalRow MakeSolutionInternalRow(
    string label,
    Orientation orientation,
    int row,
    int col
  )
  {
    Variation variation = null;

    foreach (var pwv in PiecesWithVariations.ThePiecesWithVariations)
    {
      if (pwv.Label == label)
      {
        var variationCandidate = Array.Find(pwv.Variations, v => v.Orientation == orientation);
        if (variationCandidate != null)
        {
          variation = variationCandidate;
          break;
        }
      }
    }

    if (variation != null)
    {
      var location = new Coords(row, col);
      return new DraughtboardPuzzleInternalRow(label, variation, location);
    }

    return null;
  }
}
