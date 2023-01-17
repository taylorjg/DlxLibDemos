using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.Sudoku;

namespace DlxLibDemos.Tests;

public class SudokuDemoTests
{
  [Fact]
  public void CanFindValidSolution()
  {
    var mockLogger = new NullLogger<SudokuDemo>();
    var demo = new SudokuDemo(mockLogger);

    var puzzle = Puzzles.ThePuzzles.First();
    var demoSettings = puzzle;
    var solutionInternalRows = Helpers.FindFirstSolution(demo, demoSettings);

    CheckSolution(puzzle, solutionInternalRows.Cast<SudokuInternalRow>().ToArray());
  }

  private static void CheckSolution(Puzzle puzzle, SudokuInternalRow[] internalRows)
  {
    Assert.Equal(81, internalRows.Length);
    CheckRows(puzzle, internalRows);
    CheckCols(puzzle, internalRows);
    CheckBoxes(puzzle, internalRows);
  }

  private static void CheckRows(Puzzle puzzle, SudokuInternalRow[] internalRows)
  {
    foreach (var row in Enumerable.Range(0, 9))
    {
      var values = new List<int>();
      foreach (var internalRow in internalRows)
      {
        if (internalRow.Coords.Row == row)
        {
          var value = internalRow.Value;
          Assert.InRange(value, 1, 9);
          values.Add(value);
        }
      }
      Assert.Equal(9, values.Distinct().Count());
    }
  }

  private static void CheckCols(Puzzle puzzle, SudokuInternalRow[] internalRows)
  {
    foreach (var col in Enumerable.Range(0, 9))
    {
      var values = new List<int>();
      foreach (var internalRow in internalRows)
      {
        if (internalRow.Coords.Col == col)
        {
          var value = internalRow.Value;
          Assert.InRange(value, 1, 9);
          values.Add(value);
        }
      }
      Assert.Equal(9, values.Distinct().Count());
    }
  }

  private static void CheckBoxes(Puzzle puzzle, SudokuInternalRow[] internalRows)
  {
    foreach (var box in Enumerable.Range(0, 9))
    {
      var rowFrom = box / 3 * 3;
      var rowTo = rowFrom + 2;
      var colFrom = box % 3 * 3;
      var colTo = colFrom + 2;

      var values = new List<int>();
      foreach (var internalRow in internalRows)
      {
        var row = internalRow.Coords.Row;
        var col = internalRow.Coords.Col;
        if (row >= rowFrom && row <= rowTo && col >= colFrom && col <= colTo)
        {
          var value = internalRow.Value;
          Assert.InRange(value, 1, 9);
          values.Add(value);
        }
      }
      Assert.Equal(9, values.Distinct().Count());
    }
  }
}
