namespace DlxLibDemos;

public interface IWhatToDraw
{
  public object DemoSettings { get; }
  public object DemoDrawingOptions { get; }
  public object[] SolutionInternalRows { get; }
}
