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
    var internalRows = AllPossiblePiecePlacements().Where(IsValidPiecePlacement).ToArray();
    var matrixRowToString = (int[] ns) => string.Join("", ns.Select(n => n.ToString()));
    var makeInternalRowRepresentation = (AztecDiamondInternalRow internalRow) => matrixRowToString(InternalRowToMatrixRow(internalRow));
    return internalRows.DistinctBy(makeInternalRowRepresentation).ToArray();
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
    return 25 + 50 + 50;
  }

  private bool IsValidPiecePlacement(AztecDiamondInternalRow internalRow)
  {
    foreach (var horizontal in internalRow.Variation.Horizontals)
    {
      var coords = horizontal.Add(internalRow.Location);
      if (!Locations.AllHorizontals.Any(h => h == coords)) return false;
    }

    foreach (var vertical in internalRow.Variation.Verticals)
    {
      var coords = vertical.Add(internalRow.Location);
      if (!Locations.AllVerticals.Any(v => v == coords)) return false;
    }

    return true;
  }

  private IEnumerable<AztecDiamondInternalRow> AllPossiblePiecePlacements()
  {
    foreach (var pieceWithVariations in PiecesWithVariations.ThePiecesWithVariations)
    {
      foreach (var variation in pieceWithVariations.Variations)
      {
        foreach (var location in Locations.AllLocations)
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
    var pwvs = PiecesWithVariations.ThePiecesWithVariations;
    var columns = Enumerable.Repeat(0, pwvs.Length).ToArray();
    var index = Array.FindIndex(pwvs, pwv => pwv.Variations.Any(v => v == internalRow.Variation));
    if (index >= 0)
    {
      columns[index] = 1;
    }
    return columns;
  }

  private int[] MakeHorizontalsColumns(AztecDiamondInternalRow internalRow)
  {
    var columns = Enumerable.Repeat(0, Locations.AllHorizontals.Length).ToArray();
    foreach (var horizontal in internalRow.Variation.Horizontals)
    {
      var coords = horizontal.Add(internalRow.Location);
      var index = Array.FindIndex(Locations.AllHorizontals, h => h == coords);
      if (index >= 0)
      {
        columns[index] = 1;
      }
    }
    return columns;
  }

  private int[] MakeVerticalsColumns(AztecDiamondInternalRow internalRow)
  {
    var columns = Enumerable.Repeat(0, Locations.AllVerticals.Length).ToArray();
    foreach (var vertical in internalRow.Variation.Verticals)
    {
      var coords = vertical.Add(internalRow.Location);
      var index = Array.FindIndex(Locations.AllVerticals, v => v == coords);
      if (index >= 0)
      {
        columns[index] = 1;
      }
    }
    return columns;
  }

  private int[] MakeJunctionsColumns(AztecDiamondInternalRow internalRow)
  {
    var columns = Enumerable.Repeat(0, Locations.AllJunctions.Length).ToArray();
    foreach (var junction in internalRow.Variation.Junctions)
    {
      var coords = junction.Add(internalRow.Location);
      var index = Array.FindIndex(Locations.AllJunctions, j => j == coords);
      if (index >= 0)
      {
        columns[index] = 1;
      }
    }
    return columns;
  }
}
