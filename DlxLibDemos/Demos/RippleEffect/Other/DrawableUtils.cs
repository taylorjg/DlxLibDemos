namespace DlxLibDemos.Demos.RippleEffect;

public static class DrawableUtils
{
  public record OutsideEdge(Coords Location1, Coords Location2);

  public static List<OutsideEdge> GatherOutsideEdges(Room room)
  {
    var cellExistsAt = (int row, int col) => Array.Exists(
      room.Cells,
      cell => cell.Row == row && cell.Col == col
    );

    var makeOutsideEdge = (int row1, int col1, int row2, int col2) =>
      new OutsideEdge(
        new Coords(row1, col1),
        new Coords(row2, col2)
      );

    var outsideEdges = new List<OutsideEdge>();

    foreach (var cell in room.Cells)
    {
      var row = cell.Row;
      var col = cell.Col;

      // top outside edge ?
      if (!cellExistsAt(row - 1, col))
      {
        outsideEdges.Add(makeOutsideEdge(row, col, row, col + 1));
      }

      // bottom outside edge ?
      if (!cellExistsAt(row + 1, col))
      {
        outsideEdges.Add(makeOutsideEdge(row + 1, col, row + 1, col + 1));
      }

      // left outside edge ?
      if (!cellExistsAt(row, col - 1))
      {
        outsideEdges.Add(makeOutsideEdge(row, col, row + 1, col));
      }

      // right outside edge ?
      if (!cellExistsAt(row, col + 1))
      {
        outsideEdges.Add(makeOutsideEdge(row, col + 1, row + 1, col + 1));
      }
    }

    return outsideEdges;
  }

  public static List<Coords> OutsideEdgesToBorderLocations(List<OutsideEdge> outsideEdges)
  {
    var borderLocations = new List<Coords>();
    var seenOutsideEdges = new List<OutsideEdge>();

    var findNextOutsideEdge = (Coords coords) =>
      outsideEdges.Except(seenOutsideEdges).FirstOrDefault(outsideEdge =>
        outsideEdge.Location1 == coords ||
        outsideEdge.Location2 == coords
      );

    var firstOutsideEdge = outsideEdges.First();
    borderLocations.Add(firstOutsideEdge.Location1);
    borderLocations.Add(firstOutsideEdge.Location2);
    seenOutsideEdges.Add(firstOutsideEdge);

    for (; ; )
    {
      var mostRecentLocation = borderLocations.Last();
      var nextOutsideEdge = findNextOutsideEdge(mostRecentLocation);
      var nextLocation = nextOutsideEdge.Location1 == mostRecentLocation
        ? nextOutsideEdge.Location2
        : nextOutsideEdge.Location1;
      if (nextLocation == borderLocations.First()) break;
      borderLocations.Add(nextLocation);
      seenOutsideEdges.Add(nextOutsideEdge);
    }

    return borderLocations;
  }
}
