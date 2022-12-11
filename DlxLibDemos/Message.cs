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

public class SolutionFoundMessage : BaseMessage
{
  public object[] SolutionInternalRows { get; private init; }
  public int SolutionCount { get; private init; }

  public SolutionFoundMessage(object[] solutionInternalRows, int solutionCount)
  {
    SolutionInternalRows = solutionInternalRows;
    SolutionCount = solutionCount;
  }
}

public class NoSolutionFoundMessage : BaseMessage
{
  public int SearchStepCount { get; private init; }

  public NoSolutionFoundMessage(int searchStepCount)
  {
    SearchStepCount = searchStepCount;
  }
}
