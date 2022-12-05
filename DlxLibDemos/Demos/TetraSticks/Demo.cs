using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.TetraSticks;

public class TetraSticksDemo : IDemo
{
  private ILogger<TetraSticksDemo> _logger;

  // TEMPORARY - this will come from DemoSettings eventually
  private const string LeavingOutPieceLabel = "L";

  public TetraSticksDemo(ILogger<TetraSticksDemo> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
  }

  public IDrawable CreateDrawable(IWhatToDraw whatToDraw)
  {
    return new TetraSticksDrawable(whatToDraw);
  }

  public object[] BuildInternalRows(object demoSettings)
  {
    return AllPossiblePiecePlacements().Where(IsValidPiecePlacement).ToArray();
  }

  public int[] InternalRowToMatrixRow(object internalRow)
  {
    var tetraSticksInternalRow = internalRow as TetraSticksInternalRow;
    var pieceColumns = MakePieceColumns(tetraSticksInternalRow);
    var horizontalsColumns = MakeHorizontalsColumns(tetraSticksInternalRow);
    var vertialsColumns = MakeVerticalsColumns(tetraSticksInternalRow);
    var junctionsColumns = MakeJunctionsColumns(tetraSticksInternalRow);
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

  private bool IsValidPiecePlacement(TetraSticksInternalRow internalRow)
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

  private IEnumerable<TetraSticksInternalRow> AllPossiblePiecePlacements()
  {
    var piecesInPlay = PiecesWithVariations.ThePiecesWithVariations.Where(p => p.Label != LeavingOutPieceLabel);

    foreach (var pieceWithVariations in piecesInPlay)
    {
      foreach (var variation in pieceWithVariations.Variations)
      {
        foreach (var location in Locations)
        {
          yield return new TetraSticksInternalRow(
            pieceWithVariations.Label,
            variation,
            location);
        }
      }
    }
  }

  private int[] MakePieceColumns(TetraSticksInternalRow internalRow)
  {
    var piecesInPlay = Pieces.ThePieces.Where(piece => piece.Label != LeavingOutPieceLabel).ToArray();
    var columns = Enumerable.Repeat(0, piecesInPlay.Length).ToArray();
    var pieceIndex = Array.FindIndex(piecesInPlay, p => p.Label == internalRow.Label);
    columns[pieceIndex] = 1;
    return columns;
  }

  private int[] MakeHorizontalsColumns(TetraSticksInternalRow internalRow)
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

  private int[] MakeVerticalsColumns(TetraSticksInternalRow internalRow)
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

  private int[] MakeJunctionsColumns(TetraSticksInternalRow internalRow)
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
