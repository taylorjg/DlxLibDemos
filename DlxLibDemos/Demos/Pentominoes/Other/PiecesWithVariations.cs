namespace DlxLibDemos.Demos.Pentominoes;

public static class PiecesWithVariations
{
  private record VariationCandidate(Orientation Orientation, bool Reflected, string[] Pattern);

  private static PieceWithVariations FindUniqueVariations(Piece piece)
  {
    var (label, pattern) = piece;

    var reflect = (VariationCandidate vc) =>
      vc with { Reflected = true, Pattern = vc.Pattern.Reflect() };

    var north = new VariationCandidate(Orientation.North, false, pattern);
    var northReflected = reflect(north);

    var east = new VariationCandidate(Orientation.East, false, north.Pattern.RotateCW());
    var eastReflected = reflect(east);

    var south = new VariationCandidate(Orientation.South, false, east.Pattern.RotateCW());
    var southReflected = reflect(south);

    var west = new VariationCandidate(Orientation.West, false, south.Pattern.RotateCW());
    var westReflected = reflect(west);

    var allVariationCandidates = new[] {
      north, northReflected,
      east, eastReflected,
      south, southReflected,
      west, westReflected
    };

    var uniqueVariationCandidates = allVariationCandidates.DistinctBy(vc => string.Join("|", vc.Pattern));

    var makeVariation = (VariationCandidate vc) => new Variation(
      vc.Orientation,
      vc.Reflected,
      vc.Pattern.ToCoordsList().ToArray()
    );

    var variations = uniqueVariationCandidates.Select(makeVariation);
    return new PieceWithVariations(label, variations.ToArray());
  }

  public static readonly PieceWithVariations[] ThePiecesWithVariations =
    Pieces.ThePieces.Select(FindUniqueVariations).ToArray();
}
