namespace DlxLibDemos.Demos.Nonogram;

public class NonogramDemoConfig : IDemoConfig
{
  private IDrawable _thumbnailDrawable;

  public NonogramDemoConfig(NonogramThumbnailDrawable thumbnailDrawable)
  {
    _thumbnailDrawable = thumbnailDrawable;
  }

  public string Name { get => DemoNames.Nonogram; }
  public string Route { get => "NonogramDemoPage"; }
  public IDrawable ThumbnailDrawable { get => _thumbnailDrawable; }
}
