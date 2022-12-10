namespace DlxLibDemos.Demos.AztecDiamond;

public static class Locations
{
  public static readonly Coords[] AllHorizontals =
    Enumerable.Range(0, 10).SelectMany(
      row => Enumerable.Range(0, 9).Select(col =>
        new Coords(row, col))
    )
    .Where(coords =>
    {
      var centreRelativeRow = (float)coords.Row - 4.5;
      var centreRelativeCol = (float)coords.Col - 4.0;
      return (Math.Abs(centreRelativeRow) + Math.Abs(centreRelativeCol) <= 5);
    })
    .ToArray();

  public static readonly Coords[] AllVerticals =
    Enumerable.Range(0, 9).SelectMany(
      row => Enumerable.Range(0, 10).Select(col =>
        new Coords(row, col))
    )
    .Where(coords =>
    {
      var centreRelativeRow = (float)coords.Row - 4.0;
      var centreRelativeCol = (float)coords.Col - 4.5;
      return (Math.Abs(centreRelativeRow) + Math.Abs(centreRelativeCol) <= 5);
    })
    .ToArray();

  public static readonly Coords[] AllJunctions =
    AllHorizontals
      .Intersect(AllVerticals)
      .Where(junction => AllHorizontals.Any(coords => coords == junction.Left()))
      .ToArray();

  public static readonly Coords[] AllLocations =
    AllHorizontals.Union(AllVerticals).ToArray();
}
