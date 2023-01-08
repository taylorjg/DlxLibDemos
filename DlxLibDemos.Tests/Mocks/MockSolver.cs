namespace DlxLibDemos.Tests;

public class MockSolver : ISolver
{
  public void Solve(
    SolverOptions options,
    Action<BaseMessage> onMessage,
    CancellationToken cancellationToken,
    IDemo demo,
    object demoSettings = null
  )
  {
  }
}
