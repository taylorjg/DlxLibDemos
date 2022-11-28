namespace DlxLibDemos.Demos.Sudoku;

public class SudokuThumbnailDrawable : SudokuDrawable
{
  public SudokuThumbnailDrawable(SudokuDemo demo)
    : base(new ThumbnailWhatToDraw(demo, Puzzles.ThePuzzles.First()))
  {
  }
}
