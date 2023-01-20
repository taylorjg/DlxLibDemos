namespace DlxLibDemos.Tests;

public static class Helpers
{
  public static object[] FindFirstSolution(IDemo demo, object demoSettings = null)
  {
    var internalRows = demo.BuildInternalRows(demoSettings, default(CancellationToken));
    var matrix = internalRows.Select(demo.InternalRowToMatrixRow).ToArray();
    var maybeNumPrimaryColumns = demo.GetNumPrimaryColumns(demoSettings);
    var dlx = new DlxLib.Dlx();
    var solutions = maybeNumPrimaryColumns.HasValue
     ? dlx.Solve(matrix, row => row, col => col, maybeNumPrimaryColumns.Value)
     : dlx.Solve(matrix, row => row, col => col);
    var firstSolution = solutions.FirstOrDefault();
    if (firstSolution == null) return new object[0]; // or throw exception ?
    var lookupInternalRow = (int internalRowIndex) => internalRows[internalRowIndex];
    return firstSolution.RowIndexes.Select(lookupInternalRow).ToArray();
  }
}
