namespace DlxLibDemos.Demos.NQueens;

public class NQueensThumbnailWhatToDraw : IWhatToDraw
{
  public object DemoSettings { get; private init; }
  public object DemoDrawingOptions { get; private init; }
  public object[] SolutionInternalRows { get; private init; }

  public NQueensThumbnailWhatToDraw()
  {
    DemoSettings = 8;

    SolutionInternalRows = new[]
    {
      new NQueensInternalRow(new Coords(0, 0)),
      new NQueensInternalRow(new Coords(1, 4)),
      new NQueensInternalRow(new Coords(2, 7)),
      new NQueensInternalRow(new Coords(3, 5)),
      new NQueensInternalRow(new Coords(4, 2)),
      new NQueensInternalRow(new Coords(5, 6)),
      new NQueensInternalRow(new Coords(6, 1)),
      new NQueensInternalRow(new Coords(7, 3))
    };
  }
}
