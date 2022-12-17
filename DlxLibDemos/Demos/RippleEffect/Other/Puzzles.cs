namespace DlxLibDemos.Demos.RippleEffect;

public static class Puzzles
{
  public static readonly Puzzle[] ThePuzzles = new Puzzle[]
  {
    ParsePuzzle(
      "Name",
      new string[] {
        "ABCCCDDD",
        "EBFGHHDI",
        "EEFGGJJK",
        "ELLGMMKK",
        "LLNOOMMK",
        "PLNQOMRR",
        "PSTOORRU",
        "VVTTTUUU"
      },
      new string[] {
        "---3----",
        "--------",
        "------2-",
        "---1-3--",
        "--1-5---",
        "-3------",
        "--------",
        "----2---"
      }
    )
  };

  private static Puzzle ParsePuzzle(string name, string[] roomLines, string[] initialValueLines)
  {
    var initialValues = ParseInitialValues(initialValueLines);
    var rooms = ParseRooms(roomLines, initialValues);
    return new Puzzle(name, rooms, initialValues);
  }

  private static Room[] ParseRooms(string[] roomLines, InitialValue[] initialValues)
  {
    var rowCount = roomLines.Length;
    var colCount = roomLines[0].Length;
    var dict = new Dictionary<char, List<Coords>>();

    foreach (var row in Enumerable.Range(0, rowCount))
    {
      foreach (var col in Enumerable.Range(0, colCount))
      {
        var label = roomLines[row][col];
        var cell = new Coords(row, col);
        if (dict.TryGetValue(label, out List<Coords> cells))
          cells.Add(cell);
        else
          dict.Add(label, new List<Coords> { cell });
      }
    }

    var rooms = new List<Room>();

    foreach (var entry in dict)
    {
      var label = entry.Key.ToString();
      var cells = entry.Value.ToArray();
      var initialValuesInThisRoom = initialValues
        .Where(initialValue => cells.Contains(initialValue.Cell))
        .ToArray();
      var room = new Room(label, cells, initialValuesInThisRoom);
      rooms.Add(room);
    }

    return rooms.ToArray();
  }

  private static InitialValue[] ParseInitialValues(string[] initialValueLines)
  {
    var initialValues = new List<InitialValue>();
    var rowCount = initialValueLines.Length;
    var colCount = initialValueLines[0].Length;

    foreach (var row in Enumerable.Range(0, rowCount))
    {
      foreach (var col in Enumerable.Range(0, colCount))
      {
        var ch = initialValueLines[row][col];
        if (int.TryParse(ch.ToString(), out int value))
        {
          var cell = new Coords(row, col);
          initialValues.Add(new InitialValue(cell, value));
        }
      }
    }

    return initialValues.ToArray();
  }
}
