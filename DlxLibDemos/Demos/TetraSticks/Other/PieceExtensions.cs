namespace DlxLibDemos.Demos.TetraSticks;

public static class PieceExtensions
{
  public static Piece Reflect(this Piece piece)
  {
    var width = piece.Horizontals.Max(h => h.Col) + 1;
    var newHorizontals = piece.Horizontals.Select(c => new Coords(c.Row, width - c.Col - 1)).ToArray();
    var newVerticals = piece.Verticals.Select(c => new Coords(c.Row, width - c.Col)).ToArray();
    var newJunctions = piece.Junctions.Select(c => new Coords(c.Row, width - c.Col)).ToArray();
    return new Piece(piece.Label, newHorizontals, newVerticals, newJunctions);
  }
  public static Piece RotateCW(this Piece piece)
  {
    return piece;
  }
}
