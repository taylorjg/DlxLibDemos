using Microsoft.Extensions.Logging.Abstractions;

namespace DlxLibDemos.Tests;

public class DemoPageBaseViewModelTests
{
  [Fact]
  public void CanConstructViewModel()
  {
    var mockLogger = new NullLogger<DemoPageBaseViewModel>();
    var mockSolver = new MockSolver();
    var mockDispatcherProvider = new MockDispatcherProvider();
    var dependencies = new DemoPageBaseViewModel.Dependencies(mockLogger, mockSolver, mockDispatcherProvider);
    var viewModel = new DemoPageBaseViewModel(dependencies);
  }
}
