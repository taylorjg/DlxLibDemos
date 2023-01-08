using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using DlxLibDemos.Demos.NQueens;

namespace DlxLibDemos.Tests;

public class DemoPageViewModelTests
{
  [Fact]
  public void CanConstructViewModel()
  {
    var tuple = ConstructViewModel();
    var viewModel = tuple.ViewModel;
    Assert.NotNull(viewModel);
  }

  [Fact]
  public void CanSolve()
  {
    var tuple = ConstructViewModel();
    var viewModel = tuple.ViewModel;
    var mockSolver = tuple.MockSolver;
    viewModel.SolveCommand.Execute(null);
    var options = new SolverOptions(false, 10);
    mockSolver.Verify(m => m.Solve(
      options,
      It.IsAny<Action<BaseMessage>>(),
      It.IsAny<CancellationToken>(),
      It.IsAny<IDemo>(),
      It.IsAny<object>()));
  }

  private (NQueensDemoPageViewModel ViewModel, Mock<ISolver> MockSolver) ConstructViewModel()
  {
    var mockLoggerDemo = new NullLogger<NQueensDemo>();
    var demo = new NQueensDemo(mockLoggerDemo);

    var mockLoggerBase = new NullLogger<DemoPageBaseViewModel>();
    var mockSolver = new Mock<ISolver>();
    var mockDispatcherProvider = new MockDispatcherProvider();
    var dependencies = new DemoPageBaseViewModel.Dependencies(mockLoggerBase, mockSolver.Object, mockDispatcherProvider);

    var mockLoggerDerived = new NullLogger<NQueensDemoPageViewModel>();
    var viewModel = new NQueensDemoPageViewModel(mockLoggerDerived, demo, dependencies);

    return (viewModel, mockSolver);
  }
}
