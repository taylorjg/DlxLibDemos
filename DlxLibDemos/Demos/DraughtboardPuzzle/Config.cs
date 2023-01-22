namespace DlxLibDemos.Demos.DraughtboardPuzzle;

public class DraughtboardPuzzleDemoConfig : IDemoConfig
{
  private IDrawable _thumbnailDrawable;

  public DraughtboardPuzzleDemoConfig(DraughtboardPuzzleThumbnailDrawable thumbnailDrawable)
  {
    _thumbnailDrawable = thumbnailDrawable;
  }

  public string Name { get => DemoNames.DraughtboardPuzzle; }
  public string Route { get => "DraughtboardPuzzleDemoPage"; }
  public IDrawable ThumbnailDrawable { get => _thumbnailDrawable; }
}
