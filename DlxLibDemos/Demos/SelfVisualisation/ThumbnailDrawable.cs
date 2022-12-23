namespace DlxLibDemos.Demos.SelfVisualisation;

public class SelfVisualisationThumbnailDrawable : SelfVisualisationDrawable
{
  public SelfVisualisationThumbnailDrawable(SelfVisualisationDemo demo)
    : base(new ThumbnailWhatToDraw(demo))
  {
  }
}
