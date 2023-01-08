using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Abstractions;

namespace DlxLibDemos.Tests;

public class DemoPageBaseViewModelTests
{
  [Fact]
  public void CanConstructViewModel()
  {
    ILoggerFactory loggerFactory = new NullLoggerFactory();
    var mockLogger = loggerFactory.CreateLogger<DemoPageBaseViewModel>();
    var mockSolver = new MockSolver();
    var mockDispatcherProvider = new MockDispatcherProvider();
    var dependencies = new DemoPageBaseViewModel.Dependencies(mockLogger, mockSolver, mockDispatcherProvider);
    var viewModel = new DemoPageBaseViewModel(dependencies);
  }
}
