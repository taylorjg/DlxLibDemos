namespace DlxLibDemos.Demos.Nonogram;

public static class Puzzles
{
  public static Puzzle[] ThePuzzles = new[] {
    // https://www.nonograms.org/nonograms/i/61360
    MakePuzzle(
      10,
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
    )
  };

  private static Puzzle MakePuzzle(
    int size,
    int[][] horizontalRunsData,
    int[][] verticalsRunsData
  )
  {
    var horizontalRunGroups = horizontalRunsData
      .Select((lengths, row) => new HorizontalRunGroup(lengths, row))
      .ToArray();

    var verticalRunGroups = verticalsRunsData
      .Select((lengths, col) => new VerticalRunGroup(lengths, col))
      .ToArray();

    return new Puzzle(size, horizontalRunGroups, verticalRunGroups);
  }
};
