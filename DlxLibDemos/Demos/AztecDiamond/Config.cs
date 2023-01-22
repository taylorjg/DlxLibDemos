namespace DlxLibDemos.Demos.AztecDiamond;

public class AztecDiamondDemoConfig : IDemoConfig
{
  private IDrawable _thumbnailDrawable;

  public AztecDiamondDemoConfig(AztecDiamondThumbnailDrawable thumbnailDrawable)
  {
    _thumbnailDrawable = thumbnailDrawable;
  }

  public string Name { get => DemoNames.AztecDiamond; }
  public string Route { get => "AztecDiamondDemoPage"; }
  public IDrawable ThumbnailDrawable { get => _thumbnailDrawable; }
}
