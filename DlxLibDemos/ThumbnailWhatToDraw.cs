using DlxLib;

namespace DlxLibDemos;

public class ThumbnailWhatToDraw : IWhatToDraw
{
  private IDemo _demo;
  private object _demoSettings;
  private object _demoOptionalSettings;

  public ThumbnailWhatToDraw(
    IDemo demo,
    object demoSettings = null,
    object demoOptionalSettings = null
  )
  {
    _demo = demo;
    _demoSettings = demoSettings;
    _demoOptionalSettings = demoOptionalSettings;
  }

  public object DemoSettings { get => _demoSettings; }
  public object DemoOptionalSettings { get => _demoOptionalSettings; }

  public object[] SolutionInternalRows
  {
    get
    {
      var internalRows = _demo.BuildInternalRows(DemoSettings);
      var matrix = internalRows.Select(_demo.InternalRowToMatrixRow).ToArray();
      var maybeNumPrimaryColumns = _demo.GetNumPrimaryColumns(DemoSettings);
      var dlx = new DlxLib.Dlx();
      var solutions = maybeNumPrimaryColumns.HasValue
        ? dlx.Solve(matrix, row => row, col => col, maybeNumPrimaryColumns.Value)
        : dlx.Solve(matrix, row => row, col => col);
      var solution = solutions.FirstOrDefault();
      return solution.RowIndexes.Select(rowIndex => internalRows[rowIndex]).ToArray();
    }
  }
}
