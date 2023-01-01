using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.Pentominoes;

public class PentominoesDemo : IDemo
{
  private ILogger<PentominoesDemo> _logger;

  public PentominoesDemo(ILogger<PentominoesDemo> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
  }

  public IDrawable CreateDrawable(IWhatToDraw whatToDraw)
  {
    return new PentominoesDrawable(whatToDraw);
  }

  public object[] BuildInternalRows(object demoSettings)
  {
    return AllPossiblePiecePlacements().Where(IsValidPiecePlacement).ToArray();
  }

  public int[] InternalRowToMatrixRow(object internalRow)
  {
    var pentominoesInternalRow = internalRow as PentominoesInternalRow;
    var pieceColumns = MakePieceColumns(pentominoesInternalRow);
    var locationColumns = MakeLocationColumns(pentominoesInternalRow);
    return pieceColumns.Concat(locationColumns).ToArray();
  }

  public int? GetNumPrimaryColumns(object demoSettings)
  {
    return null;
  }

  private static readonly Coords[] Locations =
    Enumerable.Range(0, 8).SelectMany(row =>
        Enumerable.Range(0, 8).Select(col =>
          new Coords(row, col))).ToArray();

  private bool IsValidPiecePlacement(PentominoesInternalRow internalRow)
  {
    foreach (var coords in internalRow.Variation.CoordsList)
    {
      var row = internalRow.Location.Row + coords.Row;
      var col = internalRow.Location.Col + coords.Col;
      if (row >= 8 || col >= 8) return false;
      if ((row == 3 || row == 4) && (col == 3 || col == 4)) return false;
    }
    return true;
  }

  private IEnumerable<PentominoesInternalRow> AllPossiblePiecePlacements()
  {
    var fixedPieces = PiecesWithVariations.ThePiecesWithVariations.Take(1).ToArray();

    foreach (var pieceWithVariations in PiecesWithVariations.ThePiecesWithVariations)
    {
      var variations = fixedPieces.Contains(pieceWithVariations)
        ? pieceWithVariations.Variations.Take(1)
        : pieceWithVariations.Variations;

      foreach (var variation in variations)
      {
        foreach (var location in Locations)
        {
          yield return new PentominoesInternalRow(
            pieceWithVariations.Label,
            variation,
            location);
        }
      }
    }
  }

  private static int[] MakePieceColumns(PentominoesInternalRow internalRow)
  {
    var columns = Enumerable.Repeat(0, Pieces.ThePieces.Length).ToArray();
    var pieceIndex = Array.FindIndex(Pieces.ThePieces, p => p.Label == internalRow.Label);
    columns[pieceIndex] = 1;
    return columns;
  }

  private static int[] MakeLocationColumns(PentominoesInternalRow internalRow)
  {
    var indices = internalRow.Variation.CoordsList.Select(coords =>
    {
      var row = internalRow.Location.Row + coords.Row;
      var col = internalRow.Location.Col + coords.Col;
      return row * 8 + col;
    });

    var columns = Enumerable.Repeat(0, 8 * 8).ToArray();

    foreach (var index in indices)
    {
      columns[index] = 1;
    }

    var locationIndicesToExclude = new[] {
      3 * 8 + 3,
      3 * 8 + 4,
      4 * 8 + 3,
      4 * 8 + 4
    };

    return columns
      .Where((_, index) => !locationIndicesToExclude.Contains(index))
      .ToArray();
  }
}
