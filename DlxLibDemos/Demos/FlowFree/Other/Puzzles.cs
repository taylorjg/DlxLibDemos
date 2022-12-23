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
    }),
    ParsePuzzle("Puzzle 2 (5x5)", new[] {
      "---AB",
      "--CB-",
      "A----",
      "EC-DE",
      "----D"
    }),

    ParsePuzzle("Puzzle 1 (6x6)", new[] {
      "-----E",
      "------",
      "-DA---",
      "---B-D",
      "-B-EAC",
      "---C--"
    }),
    ParsePuzzle("Puzzle 2 (6x6)", new[] {
      "---C-D",
      "---B-C",
      "--AD--",
      "------",
      "---BA-",
      "------"
    }),

    ParsePuzzle("Puzzle 1 (7x7)", new[] {
      "-------",
      "CD---FB",
      "EC----A",
      "--D----",
      "-----F-",
      "---E-B-",
      "A------"
    }),
    ParsePuzzle("Puzzle 2 (7x7)", new[] {
      "F-----A",
      "E-EFA-C",
      "------B",
      "-------",
      "-B-----",
      "-D---C-",
      "----D--"
    }),

    ParsePuzzle("Puzzle 1 (8x8)", new[] {
      "F--FD---",
      "--AIG-G-",
      "--C-----",
      "--B-EH--",
      "----I---",
      "---A----",
      "-C-B--ED",
      "-------H"
    }),
    ParsePuzzle("Puzzle 2 (8x8)", new[] {
      "-C-----D",
      "-H----E-",
      "----F-B-",
      "---B--D-",
      "---CA---",
      "-HE---A-",
      "----F---",
      "G------G"
    }),

    ParsePuzzle("Puzzle 1 (9x9)", new[] {
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
    ParsePuzzle("Puzzle 2 (9x9)", new[] {
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

    ParsePuzzle("Puzzle 1 (10x10)", new[] {
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
    ParsePuzzle("Puzzle 2 (10x10)", new[] {
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
