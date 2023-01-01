using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.DraughtboardPuzzle;

public class DraughtboardPuzzleDemo : IDemo
{
  private ILogger<DraughtboardPuzzleDemo> _logger;

  public DraughtboardPuzzleDemo(ILogger<DraughtboardPuzzleDemo> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
  }

  public IDrawable CreateDrawable(IWhatToDraw whatToDraw)
  {
    return new DraughtboardPuzzleDrawable(whatToDraw);
  }

  public object[] BuildInternalRows(object demoSettings)
  {
    return AllPossiblePiecePlacements().Where(IsValidPiecePlacement).ToArray();
  }

  public int[] InternalRowToMatrixRow(object internalRow)
  {
    var draughtboardPuzzleInternalRow = internalRow as DraughtboardPuzzleInternalRow;
    var pieceColumns = MakePieceColumns(draughtboardPuzzleInternalRow);
    var locationColumns = MakeLocationColumns(draughtboardPuzzleInternalRow);
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

  private bool IsValidPiecePlacement(DraughtboardPuzzleInternalRow internalRow)
  {
    foreach (var square in internalRow.Variation.Squares)
    {
      var coords = square.Coords;
      var row = internalRow.Location.Row + coords.Row;
      var col = internalRow.Location.Col + coords.Col;
      if (row >= 8 || col >= 8) return false;
      var shouldBeWhite = (row + col) % 2 != 0;
      if (shouldBeWhite && square.Colour != Colour.White) return false;
    }
    return true;
  }

  private IEnumerable<DraughtboardPuzzleInternalRow> AllPossiblePiecePlacements()
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
          yield return new DraughtboardPuzzleInternalRow(
            pieceWithVariations.Label,
            variation,
            location);
        }
      }
    }
  }

  private static int[] MakePieceColumns(DraughtboardPuzzleInternalRow internalRow)
  {
    var columns = Enumerable.Repeat(0, Pieces.ThePieces.Length).ToArray();
    var pieceIndex = Array.FindIndex(Pieces.ThePieces, p => p.Label == internalRow.Label);
    columns[pieceIndex] = 1;
    return columns;
  }

  private static int[] MakeLocationColumns(DraughtboardPuzzleInternalRow internalRow)
  {
    var indices = internalRow.Variation.Squares.Select(square =>
    {
      var coords = square.Coords;
      var row = internalRow.Location.Row + coords.Row;
      var col = internalRow.Location.Col + coords.Col;
      return row * 8 + col;
    });
    var columns = Enumerable.Repeat(0, 8 * 8).ToArray();
    foreach (var index in indices)
    {
      columns[index] = 1;
    }
    return columns;
  }
}
