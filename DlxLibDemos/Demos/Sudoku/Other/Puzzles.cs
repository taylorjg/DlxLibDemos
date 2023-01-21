namespace DlxLibDemos.Demos.Sudoku;

public static class Puzzles
{
  public static readonly Puzzle[] ThePuzzles = new Puzzle[]
  {
    new Puzzle(
      "Daily Telegraph 27744",
      ParseGrid(new string[]
      {
        "6 4 9 7 3",
        "  3    6 ",
        "       18",
        "   18   9",
        "     43  ",
        "7   39   ",
        " 7       ",
        " 4    8  ",
        "9 8 6 4 5"
      })),
    new Puzzle(
      "Daily Telegraph 27808",
      ParseGrid(new string[]
      {
        "   8    7",
        "  6 9 1  ",
        "    14 3 ",
        "  75   18",
        "     89  ",
        "25       ",
        " 6 93    ",
        "  2 6 8  ",
        "4    7   "
      })),
    new Puzzle(
      "Manchester Evening News 6th May 2016 No. 1",
      ParseGrid(new string[]
      {
        "8   2 6  ",
        " 92  4  7",
        "4    6 8 ",
        "35  6   1",
        "92 7  45 ",
        "7 62  8  ",
        "  4   29 ",
        " 7 8 2  5",
        "   6  1 4"
      })),
    new Puzzle(
      "Manchester Evening News 6th May 2016 No. 2",
      ParseGrid(new string[]
      {
        " 4 13   5",
        "1  25    ",
        "     6   ",
        "2        ",
        "6 8    5 ",
        " 9 6 1  2",
        "  7  8  1",
        "9       3",
        " 13  4 6 "
      })),
    // https://abcnews.go.com/blogs/headlines/2012/06/can-you-solve-the-hardest-ever-sudoku
    new Puzzle(
      "World's hardest Sudoku",
      ParseGrid(new string[]
      {
        "8        ",
        "  36     ",
        " 7  9 2  ",
        " 5   7   ",
        "    457  ",
        "   1   3 ",
        "  1    68",
        "  85   1 ",
        " 9    4  "
      }))
  };

  private static InitialValue[] ParseGrid(string[] grid) =>
    grid
      .SelectMany((gridRow, row) =>
        gridRow.SelectMany((ch, col) =>
        {
          var initialValues = new List<InitialValue>();
          if (int.TryParse(ch.ToString(), out int value) && value >= 1 && value <= 9)
          {
            var coords = new Coords(row, col);
            var initialValue = new InitialValue(coords, value);
            initialValues.Add(initialValue);
          }
          return initialValues;
        })
      )
      .ToArray();
}
