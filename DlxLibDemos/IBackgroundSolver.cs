namespace DlxLibDemos;

public interface IBackgroundSolver
{
  void Solve(
    bool enableSearchSteps,
    Action<BaseMessage> onMessage,
    CancellationToken cancellationToken,
    IDemo demo,
    object demoSettings = null
  );
}
