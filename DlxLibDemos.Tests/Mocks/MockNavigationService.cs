namespace DlxLibDemos.Tests;

public class MockNavigationService : INavigationService
{
  public Task GoToAsync(string route)
  {
    throw new NotImplementedException();
  }

  public Task GoToAsync(string route, IDictionary<string, object> parameters)
  {
    throw new NotImplementedException();
  }
}
