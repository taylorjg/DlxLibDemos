namespace DlxLibDemos;

public interface INavigationService
{
  public Task GoToAsync(string route);
  public Task GoToAsync(string route, IDictionary<string, object> parameters);
}
