namespace DlxLibDemos.Demos.DraughtboardPuzzle;

public static class DrawableUtils
{
  public record OutsideEdge(Coords Location1, Coords Location2);

  public static List<OutsideEdge> GatherOutsideEdges(DraughtboardPuzzleInternalRow internalRow)
  {
    var pieceExistsAt = (int row, int col) => Array.Exists(
      internalRow.Variation.Squares,
      square => square.Coords.Row == row && square.Coords.Col == col
    );

    var makeOutsideEdge = (int row1, int col1, int row2, int col2) =>
      new OutsideEdge(
        new Coords(row1 + internalRow.Location.Row, col1 + internalRow.Location.Col),
        new Coords(row2 + internalRow.Location.Row, col2 + internalRow.Location.Col)
      );

    var outsideEdges = new List<OutsideEdge>();

    foreach (var square in internalRow.Variation.Squares)
    {
      var row = square.Coords.Row;
      var col = square.Coords.Col;

      // top outside edge ?
      if (!pieceExistsAt(row - 1, col))
      {
        outsideEdges.Add(makeOutsideEdge(row, col, row, col + 1));
      }

      // bottom outside edge ?
      if (!pieceExistsAt(row + 1, col))
      {
        outsideEdges.Add(makeOutsideEdge(row + 1, col, row + 1, col + 1));
      }

      // left outside edge ?
      if (!pieceExistsAt(row, col - 1))
      {
        outsideEdges.Add(makeOutsideEdge(row, col, row + 1, col));
      }

      // right outside edge ?
      if (!pieceExistsAt(row, col + 1))
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
