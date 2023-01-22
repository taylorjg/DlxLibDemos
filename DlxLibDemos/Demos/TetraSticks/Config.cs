namespace DlxLibDemos.Demos.TetraSticks;

public class TetraSticksDemoConfig : IDemoConfig
{
  private IDrawable _thumbnailDrawable;

  public TetraSticksDemoConfig(TetraSticksThumbnailDrawable thumbnailDrawable)
  {
    _thumbnailDrawable = thumbnailDrawable;
  }

  public string Name { get => DemoNames.TetraSticks; }
  public string Route { get => "TetraSticksDemoPage"; }
  public IDrawable ThumbnailDrawable { get => _thumbnailDrawable; }
}
