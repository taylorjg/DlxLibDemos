namespace DlxLibDemos.Demos.NQueens;

public class NQueensDemoConfig : IDemoConfig
{
  private IDrawable _thumbnailDrawable;

  public NQueensDemoConfig(NQueensThumbnailDrawable thumbnailDrawable)
  {
    _thumbnailDrawable = thumbnailDrawable;
  }

  public string Name { get => DemoNames.NQueens; }
  public string Route { get => "NQueensDemoPage"; }
  public IDrawable ThumbnailDrawable { get => _thumbnailDrawable; }
}
