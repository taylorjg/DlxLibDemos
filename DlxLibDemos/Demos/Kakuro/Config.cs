namespace DlxLibDemos.Demos.Kakuro;

public class KakuroDemoConfig : IDemoConfig
{
  private IDrawable _thumbnailDrawable;

  public KakuroDemoConfig(KakuroThumbnailDrawable thumbnailDrawable)
  {
    _thumbnailDrawable = thumbnailDrawable;
  }

  public string Name { get => DemoNames.Kakuro; }
  public string Route { get => "KakuroDemoPage"; }
  public IDrawable ThumbnailDrawable { get => _thumbnailDrawable; }
}
