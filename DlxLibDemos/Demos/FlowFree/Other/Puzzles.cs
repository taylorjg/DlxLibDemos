namespace DlxLibDemos.Demos.FlowFree;

public static class Puzzles
{
  public static Puzzle[] ThePuzzles = new[] {
    ParsePuzzle("Puzzle 1 (5x5)", new[] {
      "C--AE",
      "---D-",
      "--D--",
      "-AE-B",
      "-CB--"
    })
  };

  private static Puzzle ParsePuzzle(string name, string[] grid)
  {
    var size = grid.Length;

    var dict = new Dictionary<char, List<Coords>>();
    foreach (var row in Enumerable.Range(0, size))
    {
      foreach (var col in Enumerable.Range(0, size))
      {
        var ch = grid[row][col];
        if (char.IsLetter(ch) && char.IsUpper(ch))
        {
          var coords = new Coords(row, col);
          if (dict.TryGetValue(ch, out List<Coords> coordsList))
            coordsList.Add(coords);
          else
            dict.Add(ch, new List<Coords> { coords });
        }
      }
    }

    var colourPairs = new List<ColourPair>();

    foreach (var entry in dict)
    {
      var label = entry.Key.ToString();
      var coordsList = entry.Value;
      if (coordsList.Count == 2)
      {
        var start = coordsList[0];
        var end = coordsList[1];
        var colourPair = new ColourPair(label, start, end);
        colourPairs.Add(colourPair);
      }
    }

    return new Puzzle(name, size, colourPairs.ToArray());
  }
}
