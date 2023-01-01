namespace DlxLibDemos.Demos.DraughtboardPuzzle;

public static class PiecesWithVariations
{
  private record VariationCandidate(Orientation Orientation, string[] Pattern);

  private static PieceWithVariations FindUniqueVariations(Piece piece)
  {
    var (label, pattern) = piece;

    var north = new VariationCandidate(Orientation.North, pattern);
    var east = new VariationCandidate(Orientation.East, north.Pattern.RotateCW());
    var south = new VariationCandidate(Orientation.South, east.Pattern.RotateCW());
    var west = new VariationCandidate(Orientation.West, south.Pattern.RotateCW());

    var allVariationCandidates = new[] {
      north,
      east,
      south,
      west
    };

    var uniqueVariationCandidates = allVariationCandidates.DistinctBy(vc => string.Join("|", vc.Pattern));

    var makeVariation = (VariationCandidate vc) => new Variation(
      vc.Orientation,
      vc.Pattern.ToSquares().ToArray()
    );

    var variations = uniqueVariationCandidates.Select(makeVariation);
    return new PieceWithVariations(label, variations.ToArray());
  }

  public static readonly PieceWithVariations[] ThePiecesWithVariations =
    Pieces.ThePieces.Select(FindUniqueVariations).ToArray();
}
