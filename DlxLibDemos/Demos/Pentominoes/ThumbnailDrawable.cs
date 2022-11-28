namespace DlxLibDemos.Demos.Pentominoes;

public class PentominoesThumbnailDrawable : PentominoesDrawable
{
  public PentominoesThumbnailDrawable(PentominoesDemo demo)
    : base(new ThumbnailWhatToDraw(demo, null, false))
  {
  }
}
