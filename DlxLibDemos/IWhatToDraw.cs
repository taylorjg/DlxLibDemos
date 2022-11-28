namespace DlxLibDemos;

public interface IWhatToDraw
{
  public object DemoSettings { get; }
  public object DemoOptionalSettings { get; }
  public object[] SolutionInternalRows { get; }
}
