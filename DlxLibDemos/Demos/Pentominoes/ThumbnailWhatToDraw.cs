namespace DlxLibDemos.Demos.Pentominoes;

public class PentominoesThumbnailWhatToDraw : IWhatToDraw
{
  public object DemoSettings { get; private init; }
  public object DemoDrawingOptions { get; private init; }
  public object[] SolutionInternalRows { get; private init; }

  public PentominoesThumbnailWhatToDraw()
  {
    DemoDrawingOptions = false;
    SolutionInternalRows = MakeSolution();
  }

  private static PentominoesInternalRow[] MakeSolution()
  {
    return new[]
    {
      MakeSolutionInternalRow("F", Orientation.North, false, 0, 1),
      MakeSolutionInternalRow("I", Orientation.East, false, 7, 0),
      MakeSolutionInternalRow("L", Orientation.South, false, 3, 6),
      MakeSolutionInternalRow("N", Orientation.East, false, 4, 1),
      MakeSolutionInternalRow("P", Orientation.North, true, 0, 6),
      MakeSolutionInternalRow("T", Orientation.South, false, 5, 5),
      MakeSolutionInternalRow("U", Orientation.East, false, 0, 0),
      MakeSolutionInternalRow("V", Orientation.East, false, 3, 0),
      MakeSolutionInternalRow("W", Orientation.East, false, 0, 3),
      MakeSolutionInternalRow("X", Orientation.North, false, 1, 4),
      MakeSolutionInternalRow("Y", Orientation.East, true, 5, 0),
      MakeSolutionInternalRow("Z", Orientation.North, true, 4, 4)
    };
  }

  private static PentominoesInternalRow MakeSolutionInternalRow(
    string label,
    Orientation orientation,
    bool reflected,
    int row,
    int col
  )
  {
    Variation variation = null;

    foreach (var pwv in PiecesWithVariations.ThePiecesWithVariations)
    {
      if (pwv.Label == label)
      {
        var variationCandidate = Array.Find(pwv.Variations, v => v.Orientation == orientation && v.Reflected == reflected);
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
      return new PentominoesInternalRow(label, variation, location);
    }

    return null;
  }
}
