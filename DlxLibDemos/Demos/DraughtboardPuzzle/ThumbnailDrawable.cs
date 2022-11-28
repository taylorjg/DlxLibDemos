namespace DlxLibDemos.Demos.DraughtboardPuzzle;

public class DraughtboardPuzzleThumbnailDrawable : DraughtboardPuzzleDrawable
{
  public DraughtboardPuzzleThumbnailDrawable(DraughtboardPuzzleDemo demo)
    : base(new ThumbnailWhatToDraw(demo, null, false))
  {
  }
}
