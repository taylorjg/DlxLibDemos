namespace DlxLibDemos.Tests;

public class MockDispatcher : IDispatcher
{
  public bool IsDispatchRequired => false;

  public IDispatcherTimer CreateTimer()
  {
    return new MockDispatcherTimer();
  }

  public bool Dispatch(Action action)
  {
    return false;
  }

  public bool DispatchDelayed(TimeSpan delay, Action action)
  {
    return false;
  }
}
