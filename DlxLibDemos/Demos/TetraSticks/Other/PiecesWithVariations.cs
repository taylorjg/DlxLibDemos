namespace DlxLibDemos.Demos.TetraSticks;

public static class PiecesWithVariations
{
  private static PieceWithVariations FindUniqueVariations(Piece piece)
  {
    var north = new Variation(
        Orientation.North,
        false,
        piece.Horizontals,
        piece.Verticals,
        piece.Junctions
      );
    var northReflected = north.Reflect();

    var east = north.RotateCW();
    var eastReflected = east.Reflect();

    var south = east.RotateCW();
    var southReflected = south.Reflect();

    var west = south.RotateCW();
    var westReflected = west.Reflect();

    var variations = new[] {
        north, northReflected,
        east, eastReflected,
        south, southReflected,
        west, westReflected
    };

    return new PieceWithVariations(piece.Label, variations);
  }

  public static readonly PieceWithVariations[] ThePiecesWithVariations =
    Pieces.ThePieces.Select(FindUniqueVariations).ToArray();
}
