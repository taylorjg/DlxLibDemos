namespace DlxLibDemos.Demos.FlowFree;

public record PuzzleSizeEntry(int Size, string Name);

public static class Puzzles
{
  public static Puzzle[] ThePuzzles = new[] {
    // Size 5 puzzles
    ParsePuzzle("Puzzle 1", new[] {
      "C--AE",
      "---D-",
      "--D--",
      "-AE-B",
      "-CB--"
    }),
    ParsePuzzle("Puzzle 2", new[] {
      "---AB",
      "--CB-",
      "A----",
      "EC-DE",
      "----D"
    }),

    // Size 6 puzzles
    ParsePuzzle("Puzzle 1", new[] {
      "-----E",
      "------",
      "-DA---",
      "---B-D",
      "-B-EAC",
      "---C--"
    }),
    ParsePuzzle("Puzzle 2", new[] {
      "---C-D",
      "---B-C",
      "--AD--",
      "------",
      "---BA-",
      "------"
    }),

    // Size 7 puzzles
    ParsePuzzle("Puzzle 1", new[] {
      "-------",
      "CD---FB",
      "EC----A",
      "--D----",
      "-----F-",
      "---E-B-",
      "A------"
    }),
    ParsePuzzle("Puzzle 2", new[] {
      "F-----A",
      "E-EFA-C",
      "------B",
      "-------",
      "-B-----",
      "-D---C-",
      "----D--"
    }),

    // Size 8 puzzles
    ParsePuzzle("Puzzle 1", new[] {
      "F--FD---",
      "--AIG-G-",
      "--C-----",
      "--B-EH--",
      "----I---",
      "---A----",
      "-C-B--ED",
      "-------H"
    }),
    ParsePuzzle("Puzzle 2", new[] {
      "-C-----D",
      "-H----E-",
      "----F-B-",
      "---B--D-",
      "---CA---",
      "-HE---A-",
      "----F---",
      "G------G"
    }),

    // Size 9 puzzles
    ParsePuzzle("Puzzle 1", new[] {
      "C--------",
      "---------",
      "---A-----",
      "--H--E---",
      "-----F---",
      "C-EF-G---",
      "B--D-----",
      "-G---B-H-",
      "-------AD"
    }),
    ParsePuzzle("Puzzle 2", new[] {
      "------A-A",
      "-------GB",
      "--B------",
      "------D--",
      "-F-------",
      "---------",
      "--------G",
      "EDFC----C",
      "--------E"
    }),

    // Size 10 puzzles
    ParsePuzzle("Puzzle 1", new[] {
      "----------",
      "----------",
      "-------D--",
      "-FD-----F-",
      "-E---BI-C-",
      "-----G----",
      "--AIG-----",
      "---------H",
      "-B-E-----C",
      "-H-------A"
    }),
    ParsePuzzle("Puzzle 2", new[] {
      "----------",
      "-H--------",
      "------EA--",
      "C--BJ-----",
      "----------",
      "FB--F-----",
      "--I--J-AH-",
      "----------",
      "-GD-D--GE-",
      "-------IC-"
    })
  };

  public static PuzzleSizeEntry[] ThePuzzleSizes =
    ThePuzzles
      .Select(puzzle => puzzle.Size)
      .Distinct()
      .Select(size => new PuzzleSizeEntry(size, $"{size}x{size}"))
      .ToArray();

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
