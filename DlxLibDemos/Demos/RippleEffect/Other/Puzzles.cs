namespace DlxLibDemos.Demos.RippleEffect;

public static class Puzzles
{
  // https://krazydad.com/ripple/sfiles/RIP_CH_8x8_v1_4pp_b1.pdf
  // Challenging Ripple Effect, Volume 1, Book 1
  public static readonly Puzzle[] ThePuzzles = new Puzzle[]
  {
    ParsePuzzle(
      "Ripple #1",
      new string[] {
        "ABCDDEFF",
        "ABDDGEEH",
        "ABTIGEEH",
        "AAJIGKLH",
        "AMJIIKLL",
        "NMJOPKQL",
        "NMJOPPQL",
        "NNROPPQS"
      },
      new string[] {
        "2----5--",
        "--------",
        "4-------",
        "-5------",
        "------4-",
        "-----2--",
        "--------",
        "--------"
      }
    ),
    ParsePuzzle(
      "Ripple #2",
      new string[] {
        "ABBCCCCD",
        "EEFGHICJ",
        "EFFGIIJJ",
        "EFFKIILL",
        "EMKKNLLL",
        "MMKNNOLP",
        "MMQQOOPP",
        "RQQQOOPP"
      },
      new string[] {
        "------3-",
        "-----5--",
        "-------2",
        "--4-----",
        "5----1--",
        "--------",
        "-1----1-",
        "--------"
      }
    )
  };

  private static Puzzle ParsePuzzle(string name, string[] roomLines, string[] initialValueLines)
  {
    var size = roomLines.Length;
    var initialValues = ParseInitialValues(initialValueLines);
    var rooms = ParseRooms(roomLines, initialValues);
    var maxValue = rooms.Max(room => room.Cells.Length);
    return new Puzzle(name, size, maxValue, rooms, initialValues);
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
    var roomStartIndex = 0;

    foreach (var entry in dict)
    {
      var label = entry.Key.ToString();
      var cells = entry.Value.ToArray();
      var initialValuesInThisRoom = initialValues
        .Where(initialValue => cells.Contains(initialValue.Cell))
        .ToArray();
      var room = new Room(label, cells, initialValuesInThisRoom, roomStartIndex);
      rooms.Add(room);
      roomStartIndex += cells.Length;
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
