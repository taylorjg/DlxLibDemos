namespace DlxLibDemos.Demos.Crossword;

public class CrosswordDemoConfig : IDemoConfig
{
  private IDrawable _thumbnailDrawable;

  public CrosswordDemoConfig(CrosswordThumbnailDrawable thumbnailDrawable)
  {
    _thumbnailDrawable = thumbnailDrawable;
  }

  public string Name { get => DemoNames.Crossword; }
  public string Route { get => "CrosswordDemoPage"; }
  public IDrawable ThumbnailDrawable { get => _thumbnailDrawable; }
}
