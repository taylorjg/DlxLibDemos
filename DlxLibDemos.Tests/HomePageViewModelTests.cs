using Microsoft.Extensions.Logging.Abstractions;

namespace DlxLibDemos.Tests;

public class HomePageViewModelTests
{
  [Fact]
  public void CanConstructViewModel()
  {
    var mockLogger = new NullLogger<HomePageViewModel>();
    var mockNavigationService = new MockNavigationService();
    var mockServiceProvider = new MockServiceProvider();
    var viewModel = new HomePageViewModel(mockLogger, mockNavigationService, mockServiceProvider);
  }
}
