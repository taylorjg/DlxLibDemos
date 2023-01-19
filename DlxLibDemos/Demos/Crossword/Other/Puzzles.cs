namespace DlxLibDemos.Demos.Crossword;

public static class Puzzles
{
  public static Puzzle[] ThePuzzles = new[] {
    // https://puzzles.telegraph.co.uk/print_crossword?id=49026
    MakePuzzle(
      "Telegraph Quick 30,199",
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
        { 1, new[]{ "heifer" } }, // young cow
        { 4, new[]{ "loot" } }, // swag
        { 9, new[]{ "inn", "spa" } }, // hotel
        { 10, new[]{ "heypresto" } }, // voila
        { 11, new[]{ "durable", "chronic", "abiding", "settled" } }, // long-lasting
        { 12, new[]{ "shove", "drive", "nudge" } }, // push
        { 13, new[]{ "empty", "clear" } }, // vacant
        { 15, new[]{ "lanky", "rangy", "gaunt", "weedy" } }, // tall and thin
        { 20, new[]{ "agree", "allow", "grant", "admit" } }, // be of one mind
        { 22, new[]{ "risotto", "biryani" } }, // rice dish
        { 24, new[]{ "primitive", "inelegant" } }, //  crude
        { 25, new[]{ "mug", "ass", "nit" } }, // fool
        { 26, new[]{ "dusk" } }, // twilight
        { 27, new[]{ "jester", "gagman", "mummer" } } // clown
      },
      new Dictionary<int, string[]> {
        { 1, new[]{ "hairdo" } }, // perm, e.g.
        { 2, new[]{ "inner", "chief", "focal", "prime" } }, // central
        { 3, new[]{ "exhibit", "display" } }, // put on display
        { 5, new[]{ "ogres" } }, // trolls
        { 6, new[]{ "tussock", "plumage", "topknot" } }, // tuft
        { 7, new[]{ "dyfed" } }, // welsh county
        { 8, new[]{ "yokel" } }, // bumpkin
        { 14, new[]{ "married" } }, // wed
        { 16, new[]{ "austere", "ascetic", "serious" } }, // stern
        { 17, new[]{ "happy", "merry", "jolly", "perky", "sunny" } }, // pleased
        { 18, new[]{ "trail", "dally", "tarry" } }, // lag
        { 19, new[]{ "cougar" } }, // puma
        { 21, new[]{ "epics" } }, // spice (anag)
        { 23, new[]{ "tempt", "court" } } // entice
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

    var clues = new List<Clue>();

    foreach (var kvp in acrossClues)
    {
      var (clueNumber, coordsList) = kvp;
      var candidates = acrossClueCandidates[clueNumber];
      var clue = new Clue(ClueType.Across, clueNumber, coordsList, candidates);
      clues.Add(clue);
    }

    foreach (var kvp in downClues)
    {
      var (clueNumber, coordsList) = kvp;
      var candidates = downClueCandidates[clueNumber];
      var clue = new Clue(ClueType.Down, clueNumber, coordsList, candidates);
      clues.Add(clue);
    }

    var allAcrossSquares = clues
      .Where(clue => clue.ClueType == ClueType.Across)
      .SelectMany(clue => clue.CoordsList);

    var allDownSquares = clues
      .Where(clue => clue.ClueType == ClueType.Down)
      .SelectMany(clue => clue.CoordsList);

    var crossCheckingSquares = allAcrossSquares.Intersect(allDownSquares).ToArray();

    return new Puzzle(name, size, blocks, clues.ToArray(), crossCheckingSquares);
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

    Coords[] FindCoordsList(Coords coords, Func<Coords, Coords> advance)
    {
      var coordsList = new List<Coords> { coords };
      var currentCoords = coords;
      for (; ; )
      {
        currentCoords = advance(currentCoords);
        if (isBlock(currentCoords.Row, currentCoords.Col)) break;
        coordsList.Add(currentCoords);
      }
      return coordsList.ToArray();
    }

    foreach (var row in Enumerable.Range(0, size))
    {
      foreach (var col in Enumerable.Range(0, size))
      {
        if (grid[row][col] == 'X') continue;
        var newAcrossClue = leftIsBlock(row, col) && !rightIsBlock(row, col);
        var newDownClue = upIsBlock(row, col) && !downIsBlock(row, col);
        if (newAcrossClue)
        {
          var coords = new Coords(row, col);
          var coordsList = FindCoordsList(coords, coords => coords.Right());
          acrossClues[nextClueNumber] = coordsList;
        }
        if (newDownClue)
        {
          var coords = new Coords(row, col);
          var coordsList = FindCoordsList(coords, coords => coords.Down());
          downClues[nextClueNumber] = coordsList;
        }
        if (newAcrossClue || newDownClue) nextClueNumber++;
      }
    }

    return (acrossClues, downClues);
  }
}
