namespace DlxLibDemos.Demos.FlowFree;

public class FlowFreeDemoConfig : IDemoConfig
{
  private IDrawable _thumbnailDrawable;

  public FlowFreeDemoConfig(FlowFreeThumbnailDrawable thumbnailDrawable)
  {
    _thumbnailDrawable = thumbnailDrawable;
  }

  public string Name { get => DemoNames.FlowFree; }
  public string Route { get => "FlowFreeDemoPage"; }
  public IDrawable ThumbnailDrawable { get => _thumbnailDrawable; }
}
