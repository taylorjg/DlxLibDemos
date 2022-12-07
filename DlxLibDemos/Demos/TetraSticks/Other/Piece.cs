namespace DlxLibDemos.Demos.TetraSticks;

public record Piece(
  string Label,
  Coords[] Horizontals,
  Coords[] Verticals,
  Coords[] Junctions,
  LineSegment[] LineSegments,
  int[][] Lines,
  Coords[][] PolyLines
);
