namespace DlxLibDemos.Demos.AztecDiamond;

public static class PiecesWithVariations
{
  private static PieceWithVariations MakeVariations(Piece piece, bool reflect)
  {
    var baseVariation = new Variation(
        Orientation.North,
        false,
        piece.Horizontals,
        piece.Verticals,
        piece.Junctions,
        piece.PolyLines
      );

    var north = reflect ? baseVariation.Reflect() : baseVariation;
    var east = north.RotateCW();
    var south = east.RotateCW();
    var west = south.RotateCW();

    var variations = new[] {
        north,
        east,
        south,
        west
    };

    return new PieceWithVariations(piece.Label, variations);
  }

  private static PieceWithVariations MakeUnreflectedVariations(Piece piece) => MakeVariations(piece, false);
  private static PieceWithVariations MakeReflectedVariations(Piece piece) => MakeVariations(piece, true);

  public static readonly PieceWithVariations[] ThePiecesWithVariations =
    Enumerable.Empty<PieceWithVariations>()
      .Concat(Pieces.TheTwoSidedPieces.Select(MakeUnreflectedVariations))
      .Concat(Pieces.TheOneSidedPieces.Select(MakeUnreflectedVariations))
      .Concat(Pieces.TheOneSidedPieces.Select(MakeReflectedVariations))
      .ToArray();
}
