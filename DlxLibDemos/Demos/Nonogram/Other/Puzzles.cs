namespace DlxLibDemos.Demos.Nonogram;

public static class Puzzles
{
  public static Puzzle[] ThePuzzles = new[] {
    MakePuzzle(
      10,
      new[] {
        new int[] {},
        new[] { 2, 3 },
        new int[] {},
        new int[] {},
        new int[] {},
        new[] { 4 },
        new int[] {},
        new int[] {},
        new int[] {},
        new int[] {}
      },
      new[] {
        new int[] {},
        new[] { 1 },
        new[] { 1 },
        new int[] {},
        new int[] {},
        new[] { 1 },
        new[] { 1, 1 },
        new[] { 1, 1 },
        new[] { 1, 1 },
        new int[] {}
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
