namespace DlxLibDemos.Demos.FlowFree;

public class FlowFreeThumbnailDrawable : FlowFreeDrawable
{
  public FlowFreeThumbnailDrawable(FlowFreeDemo demo)
    : base(new ThumbnailWhatToDraw(demo, Puzzles.ThePuzzles.First()))
  {
  }
}
