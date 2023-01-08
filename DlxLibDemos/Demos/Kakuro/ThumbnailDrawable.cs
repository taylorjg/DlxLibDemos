namespace DlxLibDemos.Demos.Kakuro;

public class KakuroThumbnailDrawable : KakuroDrawable
{
  public KakuroThumbnailDrawable(KakuroDemo demo)
    : base(new ThumbnailWhatToDraw(demo))
  {
  }
}
