namespace DlxLibDemos;

public interface ISolver
{
  void Solve(
    bool enableSearchSteps,
    Action<BaseMessage> onMessage,
    CancellationToken cancellationToken,
    IDemo demo,
    object demoSettings = null
  );
}
