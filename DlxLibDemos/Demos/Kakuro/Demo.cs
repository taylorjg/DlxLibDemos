using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.Kakuro;

public class KakuroDemo : IDemo
{
  private ILogger<KakuroDemo> _logger;

  public KakuroDemo(ILogger<KakuroDemo> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
  }

  public IDrawable CreateDrawable(IWhatToDraw whatToDraw)
  {
    return new KakuroDrawable(whatToDraw);
  }

  public object[] BuildInternalRows(object demoSettings, CancellationToken cancellationToken)
  {
    var puzzle = Puzzles.ThePuzzles.First();
    var internalRows = new List<KakuroInternalRow>();

    foreach (var run in puzzle.HorizontalRuns)
    {
      foreach (var setOfValues in FindRunDigits(run))
      {
        var permutations = new List<int[]>();
        Permutations.DoPermute(setOfValues, 0, setOfValues.Length - 1, permutations);
        foreach (var permutation in permutations)
        {
          var internalRow = new KakuroInternalRow(puzzle, run, permutation);
          internalRows.Add(internalRow);
        }
      }
    }

    foreach (var run in puzzle.VerticalRuns)
    {
      foreach (var setOfValues in FindRunDigits(run))
      {
        var permutations = new List<int[]>();
        Permutations.DoPermute(setOfValues, 0, setOfValues.Length - 1, permutations);
        foreach (var permutation in permutations)
        {
          var internalRow = new KakuroInternalRow(puzzle, run, permutation);
          internalRows.Add(internalRow);
        }
      }
    }

    return internalRows.ToArray();
  }

  public int[] InternalRowToMatrixRow(object internalRow)
  {
    var kakuroInternalRow = internalRow as KakuroInternalRow;
    return BuildMatrixRow(kakuroInternalRow);
  }

  public int? GetNumPrimaryColumns(object demoSettings)
  {
    var puzzle = Puzzles.ThePuzzles.First();
    return puzzle.HorizontalRuns.Length + puzzle.VerticalRuns.Length;
  }

  public int ProgressFrequency { get => 10; }

  // Return sets of values where each set of values:
  // - has length run.CoordsList.Length
  // - sums to run.Sum
  // - contains only values 1..9
  // - does not have any duplicated values
  // e.g. for run length 3 and sum 10, valid sets of values would be [1,4,5], [2,3,5], [1,3,6], etc
  private static int[][] FindRunDigits(Run run)
  {
    var setsOfValues = new List<int[]>();

    void helper(int n, Stack<List<int>> useds, Stack<int> setOfValues)
    {
      var remainingDigits = DigitsExcept(useds.SelectMany(ds => ds).ToArray());
      var used = new List<int>();
      useds.Push(used);
      foreach (var digit in remainingDigits)
      {
        setOfValues.Push(digit);
        used.Add(digit);
        if (n > 1)
        {
          helper(n - 1, useds, setOfValues);
        }
        else
        {
          if (setOfValues.Sum() == run.Sum)
          {
            setsOfValues.Add(setOfValues.ToArray());
          }
        }
        setOfValues.Pop();
      }
      useds.Pop();
    };

    var runLength = run.CoordsList.Length;
    helper(runLength, new Stack<List<int>>(), new Stack<int>());

    return setsOfValues.ToArray();
  }

  private static readonly int[] DIGITS = Enumerable.Range(1, 9).ToArray();

  private static int[] DigitsExcept(int[] ds) => DIGITS.Except(ds).ToArray();

  private int[] BuildMatrixRow(KakuroInternalRow internalRow)
  {
    var puzzle = internalRow.Puzzle;
    var run = internalRow.Run;
    var values = internalRow.Values;
    var horizontalRunColumns = MakeHorizontalRunColumns(puzzle, run);
    var verticalRunColumns = MakeVerticalRunColumns(puzzle, run);
    var horizontalRunValueColumns = MakeHorizontalRunValueColumns(puzzle, run, values);
    var verticalRunValueColumns = MakeVerticalRunValueColumns(puzzle, run, values);
    return horizontalRunColumns
      .Concat(verticalRunColumns)
      .Concat(horizontalRunValueColumns)
      .Concat(verticalRunValueColumns)
      .ToArray();
  }

  private int[] MakeHorizontalRunColumns(Puzzle puzzle, Run run)
  {
    var columns = Enumerable.Repeat(0, puzzle.HorizontalRuns.Length).ToArray();
    if (run.RunType == RunType.Horizontal)
    {
      var index = FindHorizontalRunIndex(puzzle, run);
      columns[index] = 1;
    }
    return columns;
  }

  private int[] MakeVerticalRunColumns(Puzzle puzzle, Run run)
  {
    var columns = Enumerable.Repeat(0, puzzle.HorizontalRuns.Length).ToArray();
    if (run.RunType == RunType.Vertical)
    {
      var index = FindVerticalRunIndex(puzzle, run);
      columns[index] = 1;
    }
    return columns;
  }

  private int[] MakeHorizontalRunValueColumns(Puzzle puzzle, Run run, int[] values)
  {
    var columns = Enumerable.Repeat(0, puzzle.Unknowns.Length * 9).ToArray();
    foreach (var index in Enumerable.Range(0, run.CoordsList.Length))
    {
      var value = values[index];
      var encodedValue = run.RunType == RunType.Horizontal
        ? EncodeValueNormal(value)
        : EncodeValueInverse(value);
      var unknown = run.CoordsList[index];
      var unknownIndex = FindUnknownIndex(puzzle, unknown);
      foreach (var encodedValueIndex in Enumerable.Range(0, 9))
      {
        columns[unknownIndex * 9 + encodedValueIndex] = encodedValue[encodedValueIndex];
      }
    }
    return columns;
  }

  private int[] MakeVerticalRunValueColumns(Puzzle puzzle, Run run, int[] values)
  {
    var columns = Enumerable.Repeat(0, puzzle.Unknowns.Length * 9).ToArray();
    foreach (var index in Enumerable.Range(0, run.CoordsList.Length))
    {
      var value = values[index];
      var encodedValue = run.RunType == RunType.Vertical
        ? EncodeValueNormal(value)
        : EncodeValueInverse(value);
      var unknown = run.CoordsList[index];
      var unknownIndex = FindUnknownIndex(puzzle, unknown);
      foreach (var encodedValueIndex in Enumerable.Range(0, 9))
      {
        columns[unknownIndex * 9 + encodedValueIndex] = encodedValue[encodedValueIndex];
      }
    }
    return columns;
  }

  private int[] EncodeValueNormal(int value)
  {
    var columns = Enumerable.Repeat(0, 9).ToArray();
    var index = value - 1;
    columns[index] = 1;
    return columns;
  }

  private int[] EncodeValueInverse(int value)
  {
    var columns = Enumerable.Repeat(1, 9).ToArray();
    var index = value - 1;
    columns[index] = 0;
    return columns;
  }

  private int FindHorizontalRunIndex(Puzzle puzzle, Run run)
  {
    return Array.FindIndex(puzzle.HorizontalRuns, r => r == run);
  }

  private int FindVerticalRunIndex(Puzzle puzzle, Run run)
  {
    return Array.FindIndex(puzzle.VerticalRuns, r => r == run);
  }

  private int FindUnknownIndex(Puzzle puzzle, Coords unknown)
  {
    return Array.FindIndex(puzzle.Unknowns, u => u == unknown);
  }
}
