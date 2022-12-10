namespace DlxLibDemos.Demos.AztecDiamond;

public class AztecDiamondThumbnailDrawable : AztecDiamondDrawable
{
  public AztecDiamondThumbnailDrawable(AztecDiamondDemo demo)
    : base(new ThumbnailWhatToDraw(demo))
  {
  }
}
