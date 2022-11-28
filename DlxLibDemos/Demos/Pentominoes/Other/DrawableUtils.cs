namespace DlxLibDemos.Demos.Pentominoes;

public static class DrawableUtils
{
  public record OutsideEdge(Coords Location1, Coords Location2);

  public static List<OutsideEdge> GatherOutsideEdges(PentominoesInternalRow internalRow)
  {
    var pieceExistsAt = (int row, int col) => Array.Exists(
      internalRow.Variation.CoordsList,
      coords => coords.Row == row && coords.Col == col
    );

    var makeOutsideEdge = (int row1, int col1, int row2, int col2) =>
      new OutsideEdge(
        new Coords(row1 + internalRow.Location.Row, col1 + internalRow.Location.Col),
        new Coords(row2 + internalRow.Location.Row, col2 + internalRow.Location.Col)
      );

    var outsideEdges = new List<OutsideEdge>();

    foreach (var coords in internalRow.Variation.CoordsList)
    {
      var row = coords.Row;
      var col = coords.Col;

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

  public static List<Coords> CollapseLocations(List<Coords> locations)
  {
    return locations.Where((location, index) =>
    {
      var locationBefore = index == 0 ? locations.Last() : locations[index - 1];
      var locationAfter = locations[(index + 1) % locations.Count];
      var directionBefore = Direction(locationBefore, location);
      var directionAfter = Direction(location, locationAfter);
      return directionBefore != directionAfter;
    }).ToList();
  }

  private static bool IsInnerArc(List<PointF> points, int index)
  {
    var pointBefore = index == 0 ? points.Last() : points[index - 1];
    var pointAfter = points[(index + 1) % points.Count];
    var point = points[index];
    var directionBefore = Direction(pointBefore, point);
    var directionAfter = Direction(point, pointAfter);
    var directions = directionBefore + directionAfter;
    return new[] { "RD", "DL", "LU", "UR" }.Contains(directions);
  }

  private static PointF AdjustLineStartPoint(List<PointF> points, int index, float gap)
  {
    var isInnerArc = IsInnerArc(points, index);
    var maybeGap = isInnerArc ? gap : 0;
    var halfGap = gap / 2;
    var point1 = points[index];
    var point2 = points[(index + 1) % points.Count];
    var x = point1.X;
    var y = point1.Y;
    var direction = Direction(point1, point2);
    switch (direction)
    {
      case "U": return new PointF(x + halfGap, y - maybeGap);
      case "D": return new PointF(x - halfGap, y + maybeGap);
      case "L": return new PointF(x - maybeGap, y - halfGap);
      case "R": return new PointF(x + maybeGap, y + halfGap);
    }
    throw new Exception($"[DrawableUtils.AdjustLineStartPoint] unexpected situation!");
  }

  private static PointF AdjustLineEndPoint(List<PointF> points, int index, float gap)
  {
    var isInnerArc = IsInnerArc(points, (index + 1) % points.Count);
    var maybeGap = isInnerArc ? gap : 0;
    var halfGap = gap / 2;
    var point1 = points[index];
    var point2 = points[(index + 1) % points.Count];
    var x = point2.X;
    var y = point2.Y;
    switch (Direction(point1, point2))
    {
      case "U": return new PointF(x + halfGap, y + maybeGap);
      case "D": return new PointF(x - halfGap, y - maybeGap);
      case "L": return new PointF(x + maybeGap, y - halfGap);
      case "R": return new PointF(x - maybeGap, y + halfGap);
    }
    throw new Exception($"[DrawableUtils.AdjustLineEndPoint] unexpected situation!");
  }

  public static PathF CreateBorderPath(List<PointF> points, float gap)
  {
    var adjustedPoints = new List<PointF>();

    for (var index = 0; index < points.Count; index++)
    {
      adjustedPoints.Add(AdjustLineStartPoint(points, index, gap));
      adjustedPoints.Add(AdjustLineEndPoint(points, index, gap));
    }

    var path = new PathF();

    path.MoveTo(adjustedPoints[0]);
    adjustedPoints.Add(adjustedPoints[0]);

    for (var index = 1; index < adjustedPoints.Count; index++)
    {
      var point = adjustedPoints[index];

      if (index % 2 == 1)
      {
        path.LineTo(point);
      }
      else
      {
        var rx = gap / 2;
        var ry = gap / 2;
        var angle = 0f;
        var largeArcFlag = false;
        var sweepFlag = IsInnerArc(points, (index / 2) % points.Count);
        var x = point.X;
        var y = point.Y;
        var lastPointX = path.LastPoint.X;
        var lastPointY = path.LastPoint.Y;

        path.SVGArcTo(rx, ry, angle, largeArcFlag, sweepFlag, x, y, lastPointX, lastPointY);
      }
    }

    return path;
  }

  private static string Direction(Coords location1, Coords location2)
  {
    if (location1.Col == location2.Col) return location2.Row > location1.Row ? "D" : "U";
    if (location1.Row == location2.Row) return location2.Col > location1.Col ? "R" : "L";
    throw new Exception($"[DrawableUtils.Direction] unexpected situation!");
  }

  private static string Direction(PointF point1, PointF point2)
  {
    var withinTolerance = (float value) => Math.Abs(value) <= 1e-6;
    if (withinTolerance(point1.X - point2.X)) return point2.Y > point1.Y ? "D" : "U";
    if (withinTolerance(point1.Y - point2.Y)) return point2.X > point1.X ? "R" : "L";
    throw new Exception($"[DrawableUtils.Direction] unexpected situation!");
  }
}
