namespace DlxLibDemos.Demos.Crossword;

public static class Puzzles
{
  public static Puzzle[] ThePuzzles = new[] {
    // QUICK CROSSWORD NO: 30,199
    // https://puzzles.telegraph.co.uk/print_crossword?id=49026
    MakePuzzle(
      "Puzzle 1",
      new[] {
        "......X....XX",
        ".X.X.X.X.X.X.",
        "...X.........",
        ".X.X.X.X.X.X.",
        ".......X.....",
        ".XXX.X.XXX.X.",
        "X.....X.....X",
        ".X.XXX.X.XXX.",
        ".....X.......",
        ".X.X.X.X.X.X.",
        ".........X...",
        ".X.X.X.X.X.X.",
        "XX....X......"
      },
      new Dictionary<int, string[]> {
        { 1, new[]{ "heifer" } },
        { 4, new[]{ "loot" } },
        { 9, new[]{ "inn" } },
        { 10, new[]{ "heypresto" } },
        { 11, new[]{ "durable" } },
        { 12, new[]{ "shove" } },
        { 13, new[]{ "empty" } },
        { 15, new[]{ "lanky" } },
        { 20, new[]{ "agree" } },
        { 22, new[]{ "risotto" } },
        { 24, new[]{ "primitive" } },
        { 25, new[]{ "mug" } },
        { 26, new[]{ "dusk" } },
        { 27, new[]{ "jester" } }
      },
      new Dictionary<int, string[]> {
        { 1, new[]{ "hairdo" } },
        { 2, new[]{ "inner" } },
        { 3, new[]{ "exhibit" } },
        { 5, new[]{ "ogres" } },
        { 6, new[]{ "tussock" } },
        { 7, new[]{ "dyfed" } },
        { 8, new[]{ "yokel" } },
        { 14, new[]{ "married" } },
        { 16, new[]{ "austere" } },
        { 17, new[]{ "happy" } },
        { 18, new[]{ "trail" } },
        { 19, new[]{ "cougar" } },
        { 21, new[]{ "epics" } },
        { 23, new[]{ "tempt" } },
      }
    )
  };

  private static Puzzle MakePuzzle(
    string name,
    string[] grid,
    Dictionary<int, string[]> acrossClueCandidates,
    Dictionary<int, string[]> downClueCandidates
  )
  {
    var size = grid.Length;
    var blocks = FindBlocks(grid);
    var (acrossClues, downClues) = FindClues(grid);

    return new Puzzle(
      name,
      size,
      blocks,
      acrossClues,
      downClues,
      acrossClueCandidates,
      downClueCandidates);
  }

  private static Coords[] FindBlocks(string[] grid)
  {
    var blocks = new List<Coords>();
    var size = grid.Length;

    foreach (var row in Enumerable.Range(0, size))
    {
      foreach (var col in Enumerable.Range(0, size))
      {
        if (grid[row][col] == 'X')
        {
          var coords = new Coords(row, col);
          blocks.Add(coords);
        }
      }
    }

    return blocks.ToArray();
  }

  private static (Dictionary<int, Coords[]>, Dictionary<int, Coords[]>) FindClues(string[] grid)
  {
    var acrossClues = new Dictionary<int, Coords[]>();
    var downClues = new Dictionary<int, Coords[]>();

    var size = grid.Length;
    var isBlock = (int row, int col) => row < 0 || row >= size || col < 0 || col >= size || grid[row][col] == 'X';
    var leftIsBlock = (int row, int col) => isBlock(row, col - 1);
    var rightIsBlock = (int row, int col) => isBlock(row, col + 1);
    var upIsBlock = (int row, int col) => isBlock(row - 1, col);
    var downIsBlock = (int row, int col) => isBlock(row + 1, col);

    var nextClueNumber = 1;

    foreach (var row in Enumerable.Range(0, size))
    {
      foreach (var col in Enumerable.Range(0, size))
      {
        if (grid[row][col] == 'X') continue;
        var newAcrossClue = leftIsBlock(row, col) && !rightIsBlock(row, col);
        var newDownClue = upIsBlock(row, col) && !downIsBlock(row, col);
        if (newAcrossClue)
        {
          var coordsList = new[] { new Coords(row, col) };
          acrossClues[nextClueNumber] = coordsList;
        }
        if (newDownClue)
        {
          var coordsList = new[] { new Coords(row, col) };
          downClues[nextClueNumber] = coordsList;
        }
        if (newAcrossClue || newDownClue) nextClueNumber++;
      }
    }

    return (acrossClues, downClues);
  }
}
