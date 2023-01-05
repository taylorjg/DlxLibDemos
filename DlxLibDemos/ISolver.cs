namespace DlxLibDemos;

public record SolverOptions(
  bool EnableSearchSteps,
  int ProgressFrequency
);

public interface ISolver
{
  void Solve(
    SolverOptions options,
    Action<BaseMessage> onMessage,
    CancellationToken cancellationToken,
    IDemo demo,
    object demoSettings = null
  );
}
