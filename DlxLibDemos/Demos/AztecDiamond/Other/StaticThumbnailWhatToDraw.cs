namespace DlxLibDemos.Demos.AztecDiamond;

public class AztecDiamondStaticThumbnailWhatToDraw : IWhatToDraw
{
  public object DemoSettings { get; private init; }
  public object DemoOptionalSettings { get; private init; }
  public object[] SolutionInternalRows { get; private init; }

  public AztecDiamondStaticThumbnailWhatToDraw()
  {
    SolutionInternalRows = new[]
    {
      MakeSolutionInternalRow("I", Orientation.North, false, 3, 2),
      MakeSolutionInternalRow("O", Orientation.North, false, 4, 8),
      MakeSolutionInternalRow("T", Orientation.North, false, 3, 5),
      MakeSolutionInternalRow("U", Orientation.West, false, 2, 6),
      MakeSolutionInternalRow("V", Orientation.South, false, 1, 4),
      MakeSolutionInternalRow("W", Orientation.East, false, 2, 1),
      MakeSolutionInternalRow("X", Orientation.North, false, 5, 2),

      MakeSolutionInternalRow("F", Orientation.West, false, 5, 6),
      MakeSolutionInternalRow("H", Orientation.South, false, 2, 4),
      MakeSolutionInternalRow("J", Orientation.South, false, 0, 4),
      MakeSolutionInternalRow("L", Orientation.North, false, 6, 4),
      MakeSolutionInternalRow("N", Orientation.East, false, 3, 3),
      MakeSolutionInternalRow("P", Orientation.North, false, 3, 7),
      MakeSolutionInternalRow("R", Orientation.East, false, 3, 2),
      MakeSolutionInternalRow("Y", Orientation.East, false, 1, 3),
      MakeSolutionInternalRow("Z", Orientation.East, false, 6, 5),

      MakeSolutionInternalRow("F", Orientation.South, true, 4, 1),
      MakeSolutionInternalRow("H", Orientation.West, true, 7, 2),
      MakeSolutionInternalRow("J", Orientation.East, true, 4, 0),
      MakeSolutionInternalRow("L", Orientation.West, true, 5, 5),
      MakeSolutionInternalRow("N", Orientation.North, true, 4, 5),
      MakeSolutionInternalRow("P", Orientation.South, true, 5, 4),
      MakeSolutionInternalRow("R", Orientation.East, true, 7, 4),
      MakeSolutionInternalRow("Y", Orientation.West, true, 4, 2),
      MakeSolutionInternalRow("Z", Orientation.North, true, 1, 2),
    };
  }

  private AztecDiamondInternalRow MakeSolutionInternalRow(
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
      return new AztecDiamondInternalRow(label, variation, location);
    }

    return null;
  }
}
