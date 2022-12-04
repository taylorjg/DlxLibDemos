namespace DlxLibDemos.Demos.TetraSticks;

public record Variation(
  Orientation Orientation,
  bool Reflected,
  Coords[] Horizontals,
  Coords[] Verticals,
  Coords[] Junctions
)
{
  private readonly int _width = Horizontals.Any() ? Horizontals.Max(h => h.Col) + 1 : 0;
  private readonly int _height = Verticals.Any() ? Verticals.Max(v => v.Col) + 1 : 0;

  public int Width { get => _width; }
  public int Height { get => _height; }

  public Variation Reflect()
  {
    var newHorizontals = Horizontals.Select(c => new Coords(c.Row, Width - c.Col - 1)).ToArray();
    var newVerticals = Verticals.Select(c => new Coords(c.Row, Width - c.Col)).ToArray();
    var newJunctions = Junctions.Select(c => new Coords(c.Row, Width - c.Col)).ToArray();

    return new Variation(
      Orientation,
      !Reflected,
      newHorizontals,
      newVerticals,
      newJunctions
    );
  }
};
