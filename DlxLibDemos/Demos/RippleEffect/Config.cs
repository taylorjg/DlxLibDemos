namespace DlxLibDemos.Demos.RippleEffect;

public class RippleEffectDemoConfig : IDemoConfig
{
  private IDrawable _thumbnailDrawable;

  public RippleEffectDemoConfig(RippleEffectThumbnailDrawable thumbnailDrawable)
  {
    _thumbnailDrawable = thumbnailDrawable;
  }

  public string Name { get => DemoNames.RippleEffect; }
  public string Route { get => "RippleEffectDemoPage"; }
  public IDrawable ThumbnailDrawable { get => _thumbnailDrawable; }
}
