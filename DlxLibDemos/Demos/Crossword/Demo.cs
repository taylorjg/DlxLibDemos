using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.Crossword;

public class CrosswordDemo : IDemo
{
  private ILogger<CrosswordDemo> _logger;

  public CrosswordDemo(ILogger<CrosswordDemo> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
  }

  public IDrawable CreateDrawable(IWhatToDraw whatToDraw)
  {
    return new CrosswordDrawable(whatToDraw);
  }

  public object[] BuildInternalRows(object demoSettings, CancellationToken cancellationToken)
  {
    var puzzle = Puzzles.ThePuzzles.First();
    var internalRows = new List<CrosswordInternalRow>();

    foreach (var clue in puzzle.Clues)
    {
      foreach (var candidate in clue.Candidates)
      {
        var internalRow = new CrosswordInternalRow(puzzle, clue, candidate);
        internalRows.Add(internalRow);
      }
    }

    return internalRows.ToArray();
  }

  public int[] InternalRowToMatrixRow(object internalRow)
  {
    return MakeColumns(internalRow as CrosswordInternalRow);
  }

  public int? GetNumPrimaryColumns(object demoSettings)
  {
    return null;
  }

  public int ProgressFrequency { get => 1; }

  private static int[] MakeColumns(CrosswordInternalRow internalRow)
  {
    var crossCheckingSquares = internalRow.Puzzle.CrossCheckingSquares;
    var columns = Enumerable.Repeat(0, crossCheckingSquares.Length * 26).ToArray();

    int FindCrossCheckingSquareIndex(Coords coords)
    {
      foreach (var index in Enumerable.Range(0, crossCheckingSquares.Length))
      {
        if (crossCheckingSquares[index] == coords) return index;
      }

      return -1;
    }

    var clue = internalRow.Clue;

    foreach (var index in Enumerable.Range(0, clue.CoordsList.Length))
    {
      var coords = clue.CoordsList[index];
      var crossCheckingSquareIndex = FindCrossCheckingSquareIndex(coords);
      if (crossCheckingSquareIndex >= 0)
      {
        var letter = internalRow.Candidate.ToCharArray()[index];
        var encodedLetterColumns = EncodeLetter(letter, clue.ClueType);
        var baseIndex = crossCheckingSquareIndex * 26;
        foreach (var encodedLetterIndex in Enumerable.Range(0, encodedLetterColumns.Length))
        {
          columns[baseIndex + encodedLetterIndex] = encodedLetterColumns[encodedLetterIndex];
        }
      }
    }

    return columns;
  }

  private static int[] EncodeLetter(char letter, ClueType clueType)
  {
    var upperLetter = char.ToUpper(letter);
    var index = upperLetter - 'A';
    var (on, off) = clueType == ClueType.Across ? (1, 0) : (0, 1);
    var columns = Enumerable.Repeat(off, 26).ToArray();
    columns[index] = on;
    return columns;
  }
}
