using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using DlxLibDemos.Demos.Sudoku;
using DlxLibDemos.Demos.Pentominoes;
using DlxLibDemos.Demos.NQueens;
using DlxLibDemos.Demos.DraughtboardPuzzle;
using DlxLibDemos.Demos.TetraSticks;
using DlxLibDemos.Demos.AztecDiamond;
using DlxLibDemos.Demos.RippleEffect;
using DlxLibDemos.Demos.FlowFree;
using DlxLibDemos.Demos.Kakuro;
using DlxLibDemos.Demos.Nonogram;
using DlxLibDemos.Demos.Crossword;

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
    var (viewModel, mockNavigationService) = ConstructViewModel();
    await viewModel.NavigateToDemoCommand.ExecuteAsync("PentominoesDemoPage");
    mockNavigationService.Verify(m => m.GoToAsync("PentominoesDemoPage"));
  }

  private (HomePageViewModel ViewModel, Mock<INavigationService> MockNavigationService) ConstructViewModel()
  {
    var mockLogger = new NullLogger<HomePageViewModel>();
    var mockNavigationService = new Mock<INavigationService>();

    var sudokuDemoConfig = new SudokuDemoConfig(new SudokuThumbnailDrawable());
    var pentominoesDemoConfig = new PentominoesDemoConfig(new PentominoesThumbnailDrawable());
    var nQueensDemoConfig = new NQueensDemoConfig(new NQueensThumbnailDrawable());
    var draughtboardPuzzleDemoConfig = new DraughtboardPuzzleDemoConfig(new DraughtboardPuzzleThumbnailDrawable());
    var tetraSticksDemoConfig = new TetraSticksDemoConfig(new TetraSticksThumbnailDrawable());
    var aztecDiamondDemoConfig = new AztecDiamondDemoConfig(new AztecDiamondThumbnailDrawable());
    var rippleEffectDemoConfig = new RippleEffectDemoConfig(new RippleEffectThumbnailDrawable());
    var flowFreeDemoConfig = new FlowFreeDemoConfig(new FlowFreeThumbnailDrawable());
    var kakuroDemoConfig = new KakuroDemoConfig(new KakuroThumbnailDrawable());
    var nonogramDemoConfig = new NonogramDemoConfig(new NonogramThumbnailDrawable());
    var crosswordDemoConfig = new CrosswordDemoConfig(new CrosswordThumbnailDrawable());

    var viewModel = new HomePageViewModel(
      mockLogger,
      mockNavigationService.Object,
      sudokuDemoConfig,
      pentominoesDemoConfig,
      nQueensDemoConfig,
      draughtboardPuzzleDemoConfig,
      tetraSticksDemoConfig,
      aztecDiamondDemoConfig,
      rippleEffectDemoConfig,
      flowFreeDemoConfig,
      kakuroDemoConfig,
      nonogramDemoConfig,
      crosswordDemoConfig);

    return (viewModel, mockNavigationService);
  }
}
