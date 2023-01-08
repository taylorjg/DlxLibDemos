namespace DlxLibDemos.Tests;

public class MockDispatcherProvider : IDispatcherProvider
{
  public IDispatcher GetForCurrentThread()
  {
    return new MockDispatcher();
  }
}
