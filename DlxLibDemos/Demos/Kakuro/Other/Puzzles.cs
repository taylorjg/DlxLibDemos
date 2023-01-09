namespace DlxLibDemos.Demos.Kakuro;

public static class Puzzles
{
  public static Puzzle[] ThePuzzles = new Puzzle[] {
    ParsePuzzle(
      new[] {
        "--AB-CD-EF",
        "-G..H..I..",
        "J...K.....",
        "L....M...-",
        "N..O....PQ",
        "R..S...T..",
        "--U....V..",
        "-W...X....",
        "Y.....Z...",
        "a..b..c..-"
      },
      "A:-,19 B:-,18 C:-,14 D:-,35 E:-,22 F:-,17 " +
      "G:16,15 H:9,- I:16,24 " +
      "J:7,- K:35,31 " +
      "L:29,- M:24,11 " +
      "N:3,- O:21,- P:-,26 Q:-,16 " +
      "R:4,- S:17,9 T:7,- " +
      "U:11,17 V:17,6 " +
      "W:21,7 X:12,8 " +
      "Y:25,- Z:9,- " +
      "a:4,- b:4,- c:4,-"
    )
  };

  private static Puzzle ParsePuzzle(string[] grid, string cluesString)
  {
    var size = grid.Length;
    var labelDict = MakeLabelDict(size, grid);
    var blocks = FindBlocks(size, grid);
    var clues = ParseClues(labelDict, cluesString);
    var unknowns = FindUnknowns(size, grid);
    var horizontalRuns = FindHorizontalRuns(unknowns, clues);
    var verticalRuns = FindVerticalRuns(unknowns, clues);
    return new Puzzle(size, blocks, clues, unknowns, horizontalRuns, verticalRuns);
  }

  private static Dictionary<string, Coords> MakeLabelDict(int size, string[] grid)
  {
    var labelDict = new Dictionary<string, Coords>();

    foreach (var row in Enumerable.Range(0, size))
    {
      foreach (var col in Enumerable.Range(0, size))
      {
        var ch = grid[row][col];
        if (char.IsLetter(ch))
        {
          labelDict[ch.ToString()] = new Coords(row, col);
        }
      }
    }

    return labelDict;
  }

  private static Coords[] FindBlocks(int size, string[] grid)
  {
    var blocks = new List<Coords>();

    foreach (var row in Enumerable.Range(0, size))
    {
      foreach (var col in Enumerable.Range(0, size))
      {
        var ch = grid[row][col];
        if (ch == '-' || char.IsLetter(ch))
        {
          blocks.Add(new Coords(row, col));
        }
      }
    }

    return blocks.ToArray();
  }

  private static Coords[] FindUnknowns(int size, string[] grid)
  {
    var unknowns = new List<Coords>();

    foreach (var row in Enumerable.Range(0, size))
    {
      foreach (var col in Enumerable.Range(0, size))
      {
        var ch = grid[row][col];
        if (ch == '.')
        {
          unknowns.Add(new Coords(row, col));
        }
      }
    }

    return unknowns.ToArray();
  }

  private static Coords[][] FindHorizontalRuns(Coords[] unknowns, Clue[] clues)
  {
    return clues
      .Where(clue => clue.AcrossSum.HasValue)
      .Select(clue => clue.Coords)
      .Select(startingPoint => FindRun(unknowns, coords => coords.Right(), startingPoint))
      .ToArray();
  }

  private static Coords[][] FindVerticalRuns(Coords[] unknowns, Clue[] clues)
  {
    return clues
      .Where(clue => clue.DownSum.HasValue)
      .Select(clue => clue.Coords)
      .Select(startingPoint => FindRun(unknowns, coords => coords.Down(), startingPoint))
      .ToArray();
  }

  private static Coords[] FindRun(Coords[] unknowns, Func<Coords, Coords> advance, Coords startingPoint)
  {
    var run = new List<Coords>();

    var currentCoords = startingPoint;

    for (; ; )
    {
      currentCoords = advance(currentCoords);
      if (!unknowns.Contains(currentCoords)) break;
      run.Add(currentCoords);
    }

    return run.ToArray();
  }

  private static Clue[] ParseClues(Dictionary<string, Coords> labelDict, string cluesString)
  {
    return cluesString
      .Split(" ", StringSplitOptions.TrimEntries)
      .Select(s => ParseClue(labelDict, s))
      .ToArray();
  }

  private static Clue ParseClue(Dictionary<string, Coords> labelDict, string clueString)
  {
    var bits = clueString.Split(":", StringSplitOptions.TrimEntries);
    var label = bits[0];
    var sumsString = bits[1];

    var coords = labelDict[label];

    var parseSum = (string s) =>
    {
      var gotSum = int.TryParse(s, out int sum);
      int? result = gotSum ? sum : null;
      return result;
    };

    var sumsList = sumsString.Split(",", StringSplitOptions.TrimEntries);
    int? acrossSum = parseSum(sumsList[0]);
    int? downSum = parseSum(sumsList[1]);

    return new Clue(coords, acrossSum, downSum);
  }
}
