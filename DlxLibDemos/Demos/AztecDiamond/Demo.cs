using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.AztecDiamond;

public class AztecDiamondDemo : IDemo
{
  private ILogger<AztecDiamondDemo> _logger;

  public AztecDiamondDemo(ILogger<AztecDiamondDemo> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
  }

  public IDrawable CreateDrawable(IWhatToDraw whatToDraw)
  {
    return new AztecDiamondDrawable(whatToDraw);
  }

  public object[] BuildInternalRows(object demoSettings)
  {
    return AllPossiblePiecePlacements().Where(IsValidPiecePlacement).ToArray();
  }

  public int[] InternalRowToMatrixRow(object internalRow)
  {
    var aztecDiamondInternalRow = internalRow as AztecDiamondInternalRow;
    var pieceColumns = MakePieceColumns(aztecDiamondInternalRow);
    var horizontalsColumns = MakeHorizontalsColumns(aztecDiamondInternalRow);
    var vertialsColumns = MakeVerticalsColumns(aztecDiamondInternalRow);
    var junctionsColumns = MakeJunctionsColumns(aztecDiamondInternalRow);
    return pieceColumns
      .Concat(horizontalsColumns)
      .Concat(vertialsColumns)
      .Concat(junctionsColumns)
      .ToArray();
  }

  public int? GetNumPrimaryColumns(object demoSettings)
  {
    return 15 + 30 + 30;
  }

  private static readonly Coords[] Locations =
    Enumerable.Range(0, 6).SelectMany(row =>
        Enumerable.Range(0, 6).Select(col =>
          new Coords(row, col))).ToArray();

  private bool IsValidPiecePlacement(AztecDiamondInternalRow internalRow)
  {
    var location = internalRow.Location;

    foreach (var horizontal in internalRow.Variation.Horizontals)
    {
      var row = location.Row + horizontal.Row;
      var col = location.Col + horizontal.Col;
      if (row > 5) return false; // valid rows for horizontal line segments: 0..5
      if (col > 4) return false; // valid cols for horizontal line segments: 0..4
    }

    foreach (var vertical in internalRow.Variation.Verticals)
    {
      var row = location.Row + vertical.Row;
      var col = location.Col + vertical.Col;
      if (row > 4) return false; // valid rows for vertical line segments: 0..4
      if (col > 5) return false; // valid cols for vertical line segments: 0..5
    }

    return true;
  }

  private IEnumerable<AztecDiamondInternalRow> AllPossiblePiecePlacements()
  {
    foreach (var pieceWithVariations in PiecesWithVariations.ThePiecesWithVariations)
    {
      foreach (var variation in pieceWithVariations.Variations)
      {
        foreach (var location in Locations)
        {
          yield return new AztecDiamondInternalRow(
            pieceWithVariations.Label,
            variation,
            location);
        }
      }
    }
  }

  private int[] MakePieceColumns(AztecDiamondInternalRow internalRow)
  {
    var columns = Enumerable.Repeat(0, PiecesWithVariations.ThePiecesWithVariations.Length).ToArray();
    var pieceIndex = Array.FindIndex(PiecesWithVariations.ThePiecesWithVariations, p => p.Label == internalRow.Label);
    columns[pieceIndex] = 1;
    return columns;
  }

  private int[] MakeHorizontalsColumns(AztecDiamondInternalRow internalRow)
  {
    var location = internalRow.Location;
    var columns = Enumerable.Repeat(0, 30).ToArray();
    foreach (var horizontal in internalRow.Variation.Horizontals)
    {
      var row = location.Row + horizontal.Row;
      var col = location.Col + horizontal.Col;
      columns[row * 5 + col] = 1;
    }
    return columns;
  }

  private int[] MakeVerticalsColumns(AztecDiamondInternalRow internalRow)
  {
    var location = internalRow.Location;
    var columns = Enumerable.Repeat(0, 30).ToArray();
    foreach (var vertical in internalRow.Variation.Verticals)
    {
      var row = location.Row + vertical.Row;
      var col = location.Col + vertical.Col;
      columns[col * 5 + row] = 1;
    }
    return columns;
  }

  private int[] MakeJunctionsColumns(AztecDiamondInternalRow internalRow)
  {
    var location = internalRow.Location;
    var columns = Enumerable.Repeat(0, 16).ToArray();
    foreach (var junction in internalRow.Variation.Junctions)
    {
      var row = location.Row + junction.Row;
      var col = location.Col + junction.Col;
      if (row > 0 && row < 5 && col > 0 && col < 5)
      {
        columns[(row - 1) * 4 + col - 1] = 1;
      }
    }
    return columns;
  }
}
