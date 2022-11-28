namespace DlxLibDemos;

public interface IDemo
{
  public IDrawable CreateDrawable(IWhatToDraw whatToDraw);
  public object[] BuildInternalRows(object demoSettings);
  public int[][] BuildMatrix(object[] internalRows);
  public int? GetNumPrimaryColumns(object demoSettings);
}
