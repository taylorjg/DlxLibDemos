namespace DlxLibDemos.Demos.TetraSticks;

public class TetraSticksThumbnailWhatToDraw : IWhatToDraw
{
  public object DemoSettings { get; private init; }
  public object DemoDrawingOptions { get; private init; }
  public object[] SolutionInternalRows { get; private init; }

  public TetraSticksThumbnailWhatToDraw()
  {
    DemoSettings = TetraSticksDemoPageViewModel.TheMissingLetters.First();
    SolutionInternalRows = MakeSolution();
  }

  private static TetraSticksInternalRow[] MakeSolution()
  {
    return new[]
    {
      MakeSolutionInternalRow("F", Orientation.West, true, 4, 3),
      MakeSolutionInternalRow("I", Orientation.East, false, 0, 0),
      MakeSolutionInternalRow("J", Orientation.North, true, 3, 0),
      MakeSolutionInternalRow("L", Orientation.North, false, 0, 0),
      MakeSolutionInternalRow("N", Orientation.West, true, 4, 2),
      MakeSolutionInternalRow("O", Orientation.North, false, 0, 4),
      MakeSolutionInternalRow("P", Orientation.South, true, 2, 2),
      MakeSolutionInternalRow("R", Orientation.South, true, 0, 2),
      MakeSolutionInternalRow("T", Orientation.South, false, 2, 0),
      MakeSolutionInternalRow("U", Orientation.East, true, 3, 1),
      MakeSolutionInternalRow("V", Orientation.North, true, 0, 0),
      MakeSolutionInternalRow("W", Orientation.East, true, 2, 2),
      MakeSolutionInternalRow("X", Orientation.North, false, 0, 0),
      MakeSolutionInternalRow("Y", Orientation.North, false, 1, 4),
      MakeSolutionInternalRow("Z", Orientation.North, false, 1, 3)
    };
  }

  private static TetraSticksInternalRow MakeSolutionInternalRow(
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
      return new TetraSticksInternalRow(label, variation, location);
    }

    return null;
  }
}
