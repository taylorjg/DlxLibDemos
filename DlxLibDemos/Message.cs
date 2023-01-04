namespace DlxLibDemos;

public class BaseMessage { }

public class SearchStepMessage : BaseMessage
{
  public object[] SolutionInternalRows { get; private init; }
  public int SearchStepCount { get; private init; }

  public SearchStepMessage(object[] solutionInternalRows, int searchStepCount)
  {
    SolutionInternalRows = solutionInternalRows;
    SearchStepCount = searchStepCount;
  }
}

public class ProgressMessage : BaseMessage
{
  public int SearchStepCount { get; private init; }

  public ProgressMessage(int searchStepCount)
  {
    SearchStepCount = searchStepCount;
  }
}

public class SolutionFoundMessage : BaseMessage
{
  public object[] SolutionInternalRows { get; private init; }
  public int SearchStepCount { get; private init; }
  public int SolutionCount { get; private init; }

  public SolutionFoundMessage(object[] solutionInternalRows, int searchStepCount, int solutionCount)
  {
    SolutionInternalRows = solutionInternalRows;
    SearchStepCount = searchStepCount;
    SolutionCount = solutionCount;
  }
}

public class FinishedMessage : BaseMessage
{
  public int SearchStepCount { get; private init; }
  public int SolutionCount { get; private init; }

  public FinishedMessage(int searchStepCount, int solutionCount)
  {
    SearchStepCount = searchStepCount;
    SolutionCount = solutionCount;
  }
}
