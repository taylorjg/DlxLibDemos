namespace DlxLibDemos.Demos.RippleEffect;

public class RippleEffectThumbnailDrawable : RippleEffectDrawable
{
  public RippleEffectThumbnailDrawable(RippleEffectDemo demo)
    : base(new ThumbnailWhatToDraw(demo, Puzzles.ThePuzzles.First()))
  {
  }
}
