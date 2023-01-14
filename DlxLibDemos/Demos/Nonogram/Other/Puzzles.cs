namespace DlxLibDemos.Demos.Nonogram;

public static class Puzzles
{
  public static Puzzle[] ThePuzzles = new[] {
    // https://www.nonograms.org/nonograms/i/61360
    MakePuzzle(
      "Waving Figure",
      new[] {
        new[] { 4 },
        new[] { 1, 6 },
        new[] { 1, 4 },
        new[] { 2, 2 },
        new[] { 8 },
        new[] { 2, 2 },
        new[] { 4, 1 },
        new[] { 2, 2, 1 },
        new[] { 1, 1 },
        new[] { 3, 3 }
      },
      new[] {
        new[] { 3, 1 },
        new[] { 2, 1 },
        new[] { 1, 1, 3 },
        new[] { 3, 1, 2 },
        new[] { 7 },
        new[] { 7 },
        new[] { 3, 1, 2 },
        new[] { 1, 1, 3 },
        new[] { 2, 1 },
        new[] { 3, 1 }
      }
    ),
    // https://www.nonograms.org/nonograms/i/2847
    MakePuzzle(
      "Boat",
      new[] {
        new[] { 1 },
        new[] { 1 },
        new[] { 2, 1 },
        new[] { 2, 1 },
        new[] { 3, 2 },
        new[] { 4, 3 },
        new[] { 5, 4 },
        new[] { 1 },
        new[] { 8 },
        new[] { 6 }
      },
      new[] {
        new[] { 1 },
        new[] { 2, 1 },
        new[] { 3, 2 },
        new[] { 5, 2 },
        new[] { 7, 2 },
        new[] { 3 },
        new[] { 5, 2 },
        new[] { 3, 2 },
        new[] { 2, 1 },
        new[] { 1 }
      }
    ),
    // https://www.nonograms.org/nonograms/i/1268
    MakePuzzle(
      "Charlie Chaplin",
      new[] {
        new[] { 2, 5 },
        new[] { 2, 7 },
        new[] { 2, 7 },
        new[] { 10 },
        new[] { 14 },
        new[] { 2, 1 },
        new[] { 2, 2, 3 },
        new[] { 1, 1, 1, 1 },
        new[] { 1, 1 },
        new[] { 1, 2, 1 },
        new[] { 1, 2, 1 },
        new[] { 1, 1, 3 },
        new[] { 2, 1, 1, 1, 1 },
        new[] { 4, 3, 1, 1 },
        new[] { 6, 2, 1 }
      },
      new[] {
        new[] { 1, 1 },
        new[] { 1, 2 },
        new[] { 8, 3 },
        new[] { 7, 2, 3 },
        new[] { 1, 2, 1, 2 },
        new[] { 4, 1, 1, 1 },
        new[] { 5, 2, 1 },
        new[] { 5, 2, 1 },
        new[] { 5, 2, 1 },
        new[] { 5, 2, 1, 1 },
        new[] { 5, 1, 1, 2 },
        new[] { 9 },
        new[] { 1, 4 },
        new[] { 1, 2 },
        new[] { 2 }
      }
    ),
    // https://www.nonograms.org/nonograms/i/17595
    MakePuzzle(
      "Dog",
      new[] {
        new[] { 3, 4 },
        new[] { 1, 2, 3, 1 },
        new[] { 2, 2, 3 },
        new[] { 3, 1, 1 },
        new[] { 2, 2, 2, 1 },
        new[] { 2, 2, 1 },
        new[] { 1, 3, 4 },
        new[] { 2, 2 },
        new[] { 1, 4 },
        new[] { 1, 3 },
        new[] { 1, 1 },
        new[] { 2, 2, 2, 2 },
        new[] { 4, 2, 3 },
        new[] { 1, 3, 6 },
        new[] { 6 }
      },
      new[] {
        new[] { 5, 2 },
        new[] { 1, 6, 2 },
        new[] { 1, 1, 3, 3 },
        new[] { 2, 1, 1, 3 },
        new[] { 4, 2 },
        new[] { 1, 1, 1 },
        new[] { 1, 1, 1, 2, 1 },
        new[] { 2, 3, 3 },
        new[] { 3, 2, 2 },
        new[] { 1, 2, 1, 1 },
        new[] { 3, 3, 1, 1 },
        new[] { 1, 1, 2 },
        new[] { 1, 2, 2 },
        new[] { 4, 2 },
        new[] { 5 }
      }
    ),
    // https://www.gchq.gov.uk/information/director-gchq-s-christmas-puzzle-2015---part-1
    MakePuzzle(
      "GCHQ's Christmas Puzzle 2015",
      new[] {
        new[] { 7, 3, 1, 1, 7 },
        new[] { 1, 1, 2, 2, 1, 1 },
        new[] { 1, 3, 1, 3, 1, 1, 3, 1 },
        new[] { 1, 3, 1, 1, 6, 1, 3, 1 },
        new[] { 1, 3, 1, 5, 2, 1, 3, 1 },
        new[] { 1, 1, 2, 1, 1 },
        new[] { 7, 1, 1, 1, 1, 1, 7 },
        new[] { 3, 3 },
        new[] { 1, 2, 3, 1, 1, 3, 1, 1, 2 },
        new[] { 1, 1, 3, 2, 1, 1 },
        new[] { 4, 1, 4, 2, 1, 2 },
        new[] { 1, 1, 1, 1, 1, 4, 1, 3 },
        new[] { 2, 1, 1, 1, 2, 5 },
        new[] { 3, 2, 2, 6, 3, 1 },
        new[] { 1, 9, 1, 1, 2, 1 },
        new[] { 2, 1, 2, 2, 3, 1 },
        new[] { 3, 1, 1, 1, 1, 5, 1 },
        new[] { 1, 2, 2, 5 },
        new[] { 7, 1, 2, 1, 1, 1, 3 },
        new[] { 1, 1, 2, 1, 2, 2, 1 },
        new[] { 1, 3, 1, 4, 5, 1 },
        new[] { 1, 3, 1, 3, 10, 2 },
        new[] { 1, 3, 1, 1, 6, 6 },
        new[] { 1, 1, 2, 1, 1, 2 },
        new[] { 7, 2, 1, 2, 5 }
      },
      new[] {
        new[] { 7, 2, 1, 1, 7 },
        new[] { 1, 1, 2, 2, 1, 1 },
        new[] { 1, 3, 1, 3, 1, 3, 1, 3, 1 },
        new[] { 1, 3, 1, 1, 5, 1, 3, 1 },
        new[] { 1, 3, 1, 1, 4, 1, 3, 1 },
        new[] { 1, 1, 1, 2, 1, 1 },
        new[] { 7, 1, 1, 1, 1, 1, 7 },
        new[] { 1, 1, 3 },
        new[] { 2, 1, 2, 1, 8, 2, 1 },
        new[] { 2, 2, 1, 2, 1, 1, 1, 2 },
        new[] { 1, 7, 3, 2, 1 },
        new[] { 1, 2, 3, 1, 1, 1, 1, 1 },
        new[] { 4, 1, 1, 2, 6 },
        new[] { 3, 3, 1, 1, 1, 3, 1 },
        new[] { 1, 2, 5, 2, 2 },
        new[] { 2, 2, 1, 1, 1, 1, 1, 2, 1 },
        new[] { 1, 3, 3, 2, 1, 8, 1 },
        new[] { 6, 2, 1 },
        new[] { 7, 1, 4, 1, 1, 3 },
        new[] { 1, 1, 1, 1, 4 },
        new[] { 1, 3, 1, 3, 7, 1 },
        new[] { 1, 3, 1, 1, 1, 2, 1, 1, 4 },
        new[] { 1, 3, 1, 4, 3, 3 },
        new[] { 1, 1, 2, 2, 2, 6, 1 },
        new[] { 7, 1, 3, 2, 1, 1 }
      }
    )
  };

  private static Puzzle MakePuzzle(
    string name,
    int[][] horizontalRunsData,
    int[][] verticalsRunsData
  )
  {
    var size = horizontalRunsData.Length;
    var nameIncludingSize = $"{name} ({size}x{size})";

    var horizontalRunGroups = horizontalRunsData
      .Select((lengths, row) => new HorizontalRunGroup(lengths, row))
      .ToArray();

    var verticalRunGroups = verticalsRunsData
      .Select((lengths, col) => new VerticalRunGroup(lengths, col))
      .ToArray();

    return new Puzzle(nameIncludingSize, size, horizontalRunGroups, verticalRunGroups);
  }
};
