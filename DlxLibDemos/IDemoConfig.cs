namespace DlxLibDemos;

public interface IDemoConfig
{
  string Name { get; }
  string Route { get; }
  IDrawable ThumbnailDrawable { get; }
};
