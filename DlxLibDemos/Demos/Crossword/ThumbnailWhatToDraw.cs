namespace DlxLibDemos.Demos.Crossword;

public class CrosswordThumbnailWhatToDraw : IWhatToDraw
{
  public object DemoSettings { get; private init; }
  public object DemoDrawingOptions { get; private init; }
  public object[] SolutionInternalRows { get; private init; }

  public CrosswordThumbnailWhatToDraw()
  {
    var puzzle = Puzzles.ThePuzzles.First();

    DemoSettings = puzzle;
    DemoDrawingOptions = true;
    SolutionInternalRows = MakeSolution(puzzle);
  }

  private static CrosswordInternalRow[] MakeSolution(Puzzle puzzle)
  {
    return puzzle.Clues
      .Select(clue =>
      {
        var answer = clue.Candidates.First();
        return new CrosswordInternalRow(puzzle, clue, answer);
      })
      .ToArray();
  }
}
