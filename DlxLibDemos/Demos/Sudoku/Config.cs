namespace DlxLibDemos.Demos.Sudoku;

public class SudokuDemoConfig : IDemoConfig
{
  private IDrawable _thumbnailDrawable;

  public SudokuDemoConfig(SudokuThumbnailDrawable thumbnailDrawable)
  {
    _thumbnailDrawable = thumbnailDrawable;
  }

  public string Name { get => DemoNames.Sudoku; }
  public string Route { get => "SudokuDemoPage"; }
  public IDrawable ThumbnailDrawable { get => _thumbnailDrawable; }
}
