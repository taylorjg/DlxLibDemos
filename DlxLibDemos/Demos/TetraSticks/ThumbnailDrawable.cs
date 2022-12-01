namespace DlxLibDemos.Demos.TetraSticks;

public class TetraSticksThumbnailDrawable : TetraSticksDrawable
{
  public TetraSticksThumbnailDrawable(TetraSticksDemo demo)
    : base(new ThumbnailWhatToDraw(demo))
  {
  }
}
