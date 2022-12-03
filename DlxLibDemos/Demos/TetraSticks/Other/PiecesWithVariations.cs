namespace DlxLibDemos.Demos.TetraSticks;

public static class PiecesWithVariations
{
  private static PieceWithVariations FindUniqueVariations(Piece piece)
  {
    var makeBaseVariation = (Orientation orientation) =>
      new Variation(
        orientation,
        false,
        piece.Horizontals,
        piece.Verticals,
        piece.Junctions
      );

    var north = makeBaseVariation(Orientation.North);
    var northReflected = north.Reflect();

    var variations = new[] {
        north, northReflected
    };

    return new PieceWithVariations(piece.Label, variations);
  }

  public static readonly PieceWithVariations[] ThePiecesWithVariations =
    Pieces.ThePieces.Select(FindUniqueVariations).ToArray();
}
