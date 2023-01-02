namespace DlxLibDemos;

public interface IDemo
{
  public IDrawable CreateDrawable(IWhatToDraw whatToDraw);
  public object[] BuildInternalRows(object demoSettings, CancellationToken cancellationToken);
  public int[] InternalRowToMatrixRow(object internalRow);
  public int? GetNumPrimaryColumns(object demoSettings);
}
