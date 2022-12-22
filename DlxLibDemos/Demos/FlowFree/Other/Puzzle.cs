namespace DlxLibDemos.Demos.FlowFree;

public record Puzzle(string Name, int Size, ColourPair[] ColourPairs)
{
  private Coords[] _locations =
    Enumerable.Range(0, Size).SelectMany(row =>
      Enumerable.Range(0, Size).Select(col =>
        new Coords(row, col))).ToArray();

  private Coords[] _dots =
    ColourPairs
      .SelectMany(colourPair => new[] { colourPair.Start, colourPair.End })
      .ToArray();

  private Coords[] _emptyLocations = null;


  public Coords[] Locations { get => _locations; }
  public Coords[] Dots { get => _dots; }

  public Coords[] EmptyLocations
  {
    get
    {
      if (_emptyLocations == null)
      {
        _emptyLocations = _locations.Except(_dots).ToArray();
      }
      return _emptyLocations;
    }
  }
}
