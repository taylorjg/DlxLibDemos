using Microsoft.Extensions.Logging;
using DlxLib;

namespace DlxLibDemos;

public class BackgroundSolver : IBackgroundSolver
{
  private ILogger<BackgroundSolver> _logger;

  public BackgroundSolver(ILogger<BackgroundSolver> logger)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
  }

  public void Solve(
    bool enableSearchSteps,
    Action<BaseMessage> onMessage,
    CancellationToken cancellationToken,
    IDemo demo,
    object demoSettings = null
  )
  {
    var state = Tuple.Create(enableSearchSteps, onMessage, cancellationToken, demo, demoSettings);

    Task.Factory.StartNew(
      ThreadEntryPoint,
      state,
      cancellationToken,
      TaskCreationOptions.LongRunning,
      TaskScheduler.Default
    );
  }

  private void ThreadEntryPoint(object state)
  {
    try
    {
      _logger.LogInformation("[ThreadEntryPoint]");

      var (enableSearchSteps, onMessage, cancellationToken, demo, demoSettings) =
        (Tuple<bool, Action<BaseMessage>, CancellationToken, IDemo, object>)state;

      var internalRows = demo.BuildInternalRows(demoSettings);
      var matrix = internalRows.Select(demo.InternalRowToMatrixRow).ToArray();
      var maybeNumPrimaryColumns = demo.GetNumPrimaryColumns(demoSettings);

      _logger.LogInformation($"demoSettings: {demoSettings}");
      _logger.LogInformation($"internalRows.Length: {internalRows.Length}");
      _logger.LogInformation($"matrix size: {matrix.Length} rows by {matrix[0].Length} cols");
      _logger.LogInformation($"maybeNumPrimaryColumns: {(maybeNumPrimaryColumns.HasValue ? maybeNumPrimaryColumns.Value : "null")}");

      var findSolutionInternalRows = (IEnumerable<int> rowIndices) =>
        rowIndices.Select(rowIndex => internalRows[rowIndex]).ToArray();

      var dlx = new DlxLib.Dlx(cancellationToken);

      if (enableSearchSteps)
      {
        dlx.SearchStep += (_, e) =>
        {
          var solutionInternalRows = findSolutionInternalRows(e.RowIndexes);
          var message = new SearchStepMessage(solutionInternalRows);
          MainThread.BeginInvokeOnMainThread(() => onMessage(message));
        };
      }

      dlx.SolutionFound += (_, e) =>
      {
        var solutionInternalRows = findSolutionInternalRows(e.Solution.RowIndexes);
        var message = new SolutionFoundMessage(solutionInternalRows);
        MainThread.BeginInvokeOnMainThread(() => onMessage(message));
      };

      var solutions = maybeNumPrimaryColumns.HasValue
       ? dlx.Solve(matrix, row => row, col => col, maybeNumPrimaryColumns.Value)
       : dlx.Solve(matrix, row => row, col => col);

      var solution = solutions.FirstOrDefault();

      if (solution == null)
      {
        var message = new NoSolutionFoundMessage();
        MainThread.BeginInvokeOnMainThread(() => onMessage(message));
      }
    }
    catch (Exception ex)
    {
      _logger.LogInformation("Exception caught whilst solving");
      _logger.LogInformation(ex.ToString());
    }
  }
}
