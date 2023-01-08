namespace DlxLibDemos.Tests;

public class MockServiceProvider : IServiceProvider
{
  public object GetService(Type serviceType)
  {
    throw new NotImplementedException();
  }
}
