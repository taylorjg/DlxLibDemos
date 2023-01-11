namespace DlxLibDemos.Demos.Nonogram;

public static class Puzzles
{
  public static Puzzle[] ThePuzzles = new[] {
    MakePuzzle(
      10,
      new[] {
        new[] { 1, 2 }
      },
      new[] {
        new[] { 1, 2 }
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
