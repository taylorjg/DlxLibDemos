namespace DlxLibDemos;

public interface IDemo
{
  public IDrawable CreateDrawable(IWhatToDraw whatToDraw);
  public object[] BuildInternalRows(object demoSettings);
  public int[] InternalRowToMatrixRow(object internalRow);
  public int? GetNumPrimaryColumns(object demoSettings);
}
