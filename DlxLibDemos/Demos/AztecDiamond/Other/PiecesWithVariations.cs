namespace DlxLibDemos.Demos.AztecDiamond;

public static class PiecesWithVariations
{
  private static PieceWithVariations MakeOneSidedVariations(Piece piece)
  {
    var north = new Variation(
        Orientation.North,
        false,
        piece.Horizontals,
        piece.Verticals,
        piece.Junctions,
        piece.PolyLines
      );
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

  private static PieceWithVariations MakeTwoSidedVariations(Piece piece)
  {
    var north = new Variation(
        Orientation.North,
        false,
        piece.Horizontals,
        piece.Verticals,
        piece.Junctions,
        piece.PolyLines
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
    Enumerable.Empty<PieceWithVariations>()
      .Concat(Pieces.TheOneSidedPieces.Select(MakeOneSidedVariations))
      .Concat(Pieces.TheTwoSidedPieces.Select(MakeTwoSidedVariations))
      .ToArray();
}
