namespace DlxLibDemos.Demos.TetraSticks;

public record Variation(
  Orientation Orientation,
  bool Reflected,
  Coords[] Horizontals,
  Coords[] Verticals,
  Coords[] Junctions
)
{
  public Variation Reflect()
  {
    var width = this.Horizontals.Any() ? this.Horizontals.Max(h => h.Col) + 1 : 0;

    var newHorizontals = this.Horizontals.Select(c => new Coords(c.Row, width - c.Col - 1)).ToArray();
    var newVerticals = this.Verticals.Select(c => new Coords(c.Row, width - c.Col)).ToArray();
    var newJunctions = this.Junctions.Select(c => new Coords(c.Row, width - c.Col)).ToArray();

    return new Variation(
      this.Orientation,
      !this.Reflected,
      newHorizontals,
      newVerticals,
      newJunctions
    );
  }
};
