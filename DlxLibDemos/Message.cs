namespace DlxLibDemos;

public class BaseMessage { }

public class SearchStepMessage : BaseMessage
{
  public object[] SolutionInternalRows { get; private set; }

  public SearchStepMessage(object[] solutionInternalRows)
  {
    SolutionInternalRows = solutionInternalRows;
  }
}

public class SolutionFoundMessage : BaseMessage
{
  public object[] SolutionInternalRows { get; private set; }

  public SolutionFoundMessage(object[] solutionInternalRows)
  {
    SolutionInternalRows = solutionInternalRows;
  }
}

public class NoSolutionFoundMessage : BaseMessage { }
