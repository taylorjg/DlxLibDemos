namespace DlxLibDemos.Demos.SelfVisualisation;

public class SelfVisualisationDemoConfig : IDemoConfig
{
  private IDrawable _thumbnailDrawable;

  public SelfVisualisationDemoConfig(SelfVisualisationThumbnailDrawable thumbnailDrawable)
  {
    _thumbnailDrawable = thumbnailDrawable;
  }

  public string Name { get => DemoNames.SelfVisualisation; }
  public string Route { get => "SelfVisualisationDemoPage"; }
  public IDrawable ThumbnailDrawable { get => _thumbnailDrawable; }
}
