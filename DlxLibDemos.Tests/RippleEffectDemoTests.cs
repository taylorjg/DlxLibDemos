using Microsoft.Extensions.Logging.Abstractions;
using DlxLibDemos.Demos.RippleEffect;

namespace DlxLibDemos.Tests;

public class RippleEffectDemoTests
{
  [Fact]
  public void CanFindValidSolution()
  {
    var mockLogger = new NullLogger<RippleEffectDemo>();
    var demo = new RippleEffectDemo(mockLogger);

    var puzzle = Puzzles.ThePuzzles.First();
    var demoSettings = puzzle;
    var solutionInternalRows = Helpers.FindFirstSolution(demo, demoSettings);

    CheckSolution(puzzle, solutionInternalRows.Cast<RippleEffectInternalRow>().ToArray());
  }

  private static void CheckSolution(Puzzle puzzle, RippleEffectInternalRow[] internalRows)
  {
    var size = puzzle.Size;
    Assert.Equal(size * size, internalRows.Length);
    CheckAdjacenyConstraints(puzzle, internalRows);
    CheckRoomValues(puzzle, internalRows);
  }

  private static void CheckAdjacenyConstraints(Puzzle puzzle, RippleEffectInternalRow[] internalRows)
  {
    void CheckAdjacencyConstraint(Coords cell, int value, Func<Coords, Coords> advance)
    {
      var currentCell = cell;

      foreach (var _ in Enumerable.Range(0, value))
      {
        currentCell = advance(currentCell);
        var internalRow = internalRows.FirstOrDefault(ir => ir.Cell == currentCell);
        if (internalRow != null)
        {
          var message = $"cell: {cell}; value: {value}; currentCell: {currentCell}";
          Assert.False(internalRow.Value == value, message);
        }
      }
    }

    foreach (var internalRow in internalRows)
    {
      CheckAdjacencyConstraint(internalRow.Cell, internalRow.Value, coords => coords.Up());
      CheckAdjacencyConstraint(internalRow.Cell, internalRow.Value, coords => coords.Down());
      CheckAdjacencyConstraint(internalRow.Cell, internalRow.Value, coords => coords.Left());
      CheckAdjacencyConstraint(internalRow.Cell, internalRow.Value, coords => coords.Right());
    }
  }

  private static void CheckRoomValues(Puzzle puzzle, RippleEffectInternalRow[] internalRows)
  {
    int FindValue(Coords cell)
    {
      foreach (var internalRow in internalRows)
      {
        if (internalRow.Cell == cell) return internalRow.Value;
      }

      return 0;
    }

    foreach (var room in puzzle.Rooms)
    {
      var roomSize = room.Cells.Length;
      var values = new List<int>();
      foreach (var cell in room.Cells)
      {
        var value = FindValue(cell);
        Assert.InRange(value, 1, roomSize);
        values.Add(value);
      }
      Assert.Equal(roomSize, values.Distinct().Count());
    }
  }
}
