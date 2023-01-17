using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.NQueens;

namespace DlxLibDemos.Tests;

public class NQueensDemoTests
{
  [Fact]
  public void CanFindValidSolution()
  {
    var mockLogger = new NullLogger<NQueensDemo>();
    var demo = new NQueensDemo(mockLogger);

    var size = 8;
    var demoSettings = size;
    var solutionInternalRows = Helpers.FindFirstSolution(demo, demoSettings);

    CheckSolution(size, solutionInternalRows.Cast<NQueensInternalRow>().ToArray());
  }

  private void CheckSolution(int size, NQueensInternalRow[] internalRows)
  {
    Assert.Equal(size, internalRows.Length);

    // check rows
    var distinctRows = internalRows.Select(internalRow => internalRow.Coords.Row).Distinct();
    Assert.Equal(size, distinctRows.Count());

    // check cols
    var distinctCols = internalRows.Select(internalRow => internalRow.Coords.Col).Distinct();
    Assert.Equal(size, distinctCols.Count());

    var diagonalColumnCount = size + size - 1;

    // check diagonals from top left to bottom right
    foreach (var index in Enumerable.Range(0, diagonalColumnCount))
    {
      var diagonalCoords = new List<Coords>();
      foreach (var row in Enumerable.Range(0, size))
      {
        foreach (var col in Enumerable.Range(0, size))
        {
          if (row + col == index)
          {
            diagonalCoords.Add(new Coords(row, col));
          }
        }
      }
      var count = diagonalCoords.Count(coords => internalRows.Any(internalRow => internalRow.Coords == coords));
      Assert.InRange(count, 0, 1);
    }

    // check diagonals from top right to bottom left
    foreach (var index in Enumerable.Range(0, diagonalColumnCount))
    {
      var diagonalCoords = new List<Coords>();
      foreach (var row in Enumerable.Range(0, size))
      {
        foreach (var col in Enumerable.Range(0, size))
        {
          if (size - 1 - col + row == index)
          {
            diagonalCoords.Add(new Coords(row, col));
          }
        }
      }
      var count = diagonalCoords.Count(coords => internalRows.Any(internalRow => internalRow.Coords == coords));
      Assert.InRange(count, 0, 1);
    }
  }
}
