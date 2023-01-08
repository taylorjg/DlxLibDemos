using Microsoft.Extensions.Logging.Abstractions;
using Moq;

namespace DlxLibDemos.Tests;

public class HomePageViewModelTests
{
  [Fact]
  public void CanConstructViewModel()
  {
    var tuple = ConstructViewModel();
    Assert.NotNull(tuple.ViewModel);
  }

  [Fact]
  public async void CanNavigateToDemoPage()
  {
    var tuple = ConstructViewModel();
    await tuple.ViewModel.NavigateToDemoCommand.ExecuteAsync("PentominoesDemoPage");
    tuple.MockNavigationService.Verify(m => m.GoToAsync("PentominoesDemoPage"));
  }

  private (HomePageViewModel ViewModel, Mock<INavigationService> MockNavigationService) ConstructViewModel()
  {
    var mockLogger = new NullLogger<HomePageViewModel>();
    var mockNavigationService = new Mock<INavigationService>();
    var mockServiceProvider = new MockServiceProvider();
    var viewModel = new HomePageViewModel(mockLogger, mockNavigationService.Object, mockServiceProvider);
    return (viewModel, mockNavigationService);
  }
}
