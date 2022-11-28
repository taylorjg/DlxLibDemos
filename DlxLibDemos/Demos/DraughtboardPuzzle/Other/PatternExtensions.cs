namespace DlxLibDemos.Demos.DraughtboardPuzzle;

public static class PatternExtensions
{
  private static string CharsToString(this IEnumerable<char> chars) =>
    new string(chars.ToArray());

  public static string[] RotateCW(this string[] pattern)
  {
    var rowCount = pattern.Length;
    var colCount = pattern[0].Length;
    var rowIndices = Enumerable.Range(0, rowCount);
    var colIndices = Enumerable.Range(0, colCount);
    var transposed = colIndices.Select(colIndex =>
      rowIndices.Select(rowIndex => pattern[rowIndex][colIndex]).CharsToString())
        .ToArray();
    return transposed.Reflect();
  }

  public static string[] Reflect(this string[] pattern)
  {
    var reverseString = (string s) => s.Reverse().CharsToString();
    return pattern.Select(reverseString).ToArray();
  }

  public static IEnumerable<Square> ToSquares(this string[] pattern)
  {
    var rowCount = pattern.Length;
    var colCount = pattern[0].Length;

    foreach (var row in Enumerable.Range(0, rowCount))
    {
      foreach (var col in Enumerable.Range(0, colCount))
      {
        var coords = new Coords(row, col);
        switch (pattern[row][col])
        {
          case 'B':
            yield return new Square(coords, Colour.Black);
            break;
          case 'W':
            yield return new Square(coords, Colour.White);
            break;
        }
      }
    }
  }
}
