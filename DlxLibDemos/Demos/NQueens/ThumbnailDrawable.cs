namespace DlxLibDemos.Demos.NQueens;

public class NQueensThumbnailDrawable : NQueensDrawable
{
  public NQueensThumbnailDrawable(NQueensDemo demo)
    : base(new ThumbnailWhatToDraw(demo, 8))
  {
  }
}
