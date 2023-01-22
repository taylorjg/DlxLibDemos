namespace DlxLibDemos.Demos.Pentominoes;

public class PentominoesDemoConfig : IDemoConfig
{
  private IDrawable _thumbnailDrawable;

  public PentominoesDemoConfig(PentominoesThumbnailDrawable thumbnailDrawable)
  {
    _thumbnailDrawable = thumbnailDrawable;
  }

  public string Name { get => DemoNames.Pentominoes; }
  public string Route { get => "PentominoesDemoPage"; }
  public IDrawable ThumbnailDrawable { get => _thumbnailDrawable; }
}
