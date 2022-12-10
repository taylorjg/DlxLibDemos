namespace DlxLibDemos.Demos.AztecDiamond;

public record Variation(
  Orientation Orientation,
  bool Reflected,
  Coords[] Horizontals,
  Coords[] Verticals,
  Coords[] Junctions,
  Coords[][] PolyLines
)
{
  private readonly int _width = Horizontals.Any() ? Horizontals.Max(h => h.Col) + 1 : 0;
  private readonly int _height = Verticals.Any() ? Verticals.Max(v => v.Row) + 1 : 0;

  public int Width { get => _width; }
  public int Height { get => _height; }

  public Variation Reflect()
  {
    var newHorizontals = Horizontals.Select(c => new Coords(c.Row, Width - c.Col - 1)).ToArray();
    var newVerticals = Verticals.Select(c => new Coords(c.Row, Width - c.Col)).ToArray();
    var newJunctions = Junctions.Select(c => new Coords(c.Row, Width - c.Col)).ToArray();
    var newPolyLines = PolyLines.Select(polyLine =>
      polyLine.Select(c => new Coords(c.Row, Width - c.Col)).ToArray()).ToArray();

    return new Variation(
      Orientation,
      !Reflected,
      newHorizontals,
      newVerticals,
      newJunctions,
      newPolyLines
    );
  }

  public Variation RotateCW()
  {
    var newHorizontals = Verticals.Select(c => new Coords(c.Col, Height - c.Row - 1)).ToArray();
    var newVerticals = Horizontals.Select(c => new Coords(c.Col, Height - c.Row)).ToArray();
    var newJunctions = Junctions.Select(c => new Coords(c.Col, Height - c.Row)).ToArray();
    var newPolyLines = PolyLines.Select(polyLine =>
      polyLine.Select(c => new Coords(c.Col, Height - c.Row)).ToArray()).ToArray();

    return new Variation(
      Orientation.RotateCW(),
      Reflected,
      newHorizontals,
      newVerticals,
      newJunctions,
      newPolyLines
    );
  }

  private class CoordsComparer : IComparer<Coords>
  {
    public int Compare(Coords coords1, Coords coords2)
    {
      var rowDiff = coords1.Row - coords2.Row;
      var colDiff = coords1.Col - coords2.Col;
      return rowDiff != 0 ? rowDiff : colDiff;
    }
  }

  public string NormalisedRepresentation()
  {
    var comparer = new CoordsComparer();
    var sortedHorizontals = Horizontals.OrderBy(coords => coords, comparer);
    var sortedVerticals = Verticals.OrderBy(coords => coords, comparer);
    var hs = sortedHorizontals.Select(coords => $"H{coords.Row}{coords.Col}");
    var vs = sortedVerticals.Select(coords => $"V{coords.Row}{coords.Col}");
    return string.Join("-", hs.Concat(vs));
  }
}
