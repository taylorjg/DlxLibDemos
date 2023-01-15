using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using DlxLibDemos.Demos.Sudoku;
using DlxLibDemos.Demos.Pentominoes;
using DlxLibDemos.Demos.NQueens;
using DlxLibDemos.Demos.DraughtboardPuzzle;
using DlxLibDemos.Demos.TetraSticks;
using DlxLibDemos.Demos.AztecDiamond;
using DlxLibDemos.Demos.RippleEffect;
using DlxLibDemos.Demos.FlowFree;
using DlxLibDemos.Demos.Nonogram;
using DlxLibDemos.Demos.Kakuro;
using DlxLibDemos.Demos.Crossword;
// using DlxLibDemos.Demos.SelfVisualisation;

namespace DlxLibDemos;

public record AvailableDemo(
  string Name,
  string Route,
  IDrawable ThumbnailDrawable
);

public partial class HomePageViewModel : ObservableObject
{
  private ILogger<HomePageViewModel> _logger;
  private INavigationService _navigationService;
  private IServiceProvider _serviceProvider;

  public HomePageViewModel(
    ILogger<HomePageViewModel> logger,
    INavigationService navigationService,
    IServiceProvider serviceProvider
  )
  {
    _logger = logger;
    _navigationService = navigationService;
    _serviceProvider = serviceProvider;
    _logger.LogInformation("constructor");
  }

  public AvailableDemo[] AvailableDemos
  {
    get
    {
      return new[] {
        new AvailableDemo(
          DemoNames.Sudoku,
          "SudokuDemoPage",
          _serviceProvider.GetService<SudokuThumbnailDrawable>()
        ),
        new AvailableDemo(
          DemoNames.Pentominoes,
          "PentominoesDemoPage",
          _serviceProvider.GetService<PentominoesThumbnailDrawable>()
        ),
        new AvailableDemo(
          DemoNames.NQueens,
          "NQueensDemoPage",
          _serviceProvider.GetService<NQueensThumbnailDrawable>()
        ),
        new AvailableDemo(
          DemoNames.DraughtboardPuzzle,
          "DraughtboardPuzzleDemoPage",
          _serviceProvider.GetService<DraughtboardPuzzleThumbnailDrawable>()
        ),
        new AvailableDemo(
          DemoNames.TetraSticks,
          "TetraSticksDemoPage",
          _serviceProvider.GetService<TetraSticksThumbnailDrawable>()
        ),
        new AvailableDemo(
          DemoNames.AztecDiamond,
          "AztecDiamondDemoPage",
          _serviceProvider.GetService<AztecDiamondThumbnailDrawable>()
        ),
        new AvailableDemo(
          DemoNames.RippleEffect,
          "RippleEffectDemoPage",
          _serviceProvider.GetService<RippleEffectThumbnailDrawable>()
        ),
        new AvailableDemo(
          DemoNames.FlowFree,
          "FlowFreeDemoPage",
          _serviceProvider.GetService<FlowFreeThumbnailDrawable>()
        ),
        new AvailableDemo(
          DemoNames.Kakuro,
          "KakuroDemoPage",
          _serviceProvider.GetService<KakuroThumbnailDrawable>()
        ),
        new AvailableDemo(
          DemoNames.Nonogram,
          "NonogramDemoPage",
          _serviceProvider.GetService<NonogramThumbnailDrawable>()
        ),
        new AvailableDemo(
          DemoNames.Crossword,
          "CrosswordDemoPage",
          _serviceProvider.GetService<CrosswordThumbnailDrawable>()
        ),
        // new AvailableDemo(
        //   DemoNames.SelfVisualisation,
        //   "SelfVisualisationDemoPage",
        //   _serviceProvider.GetService<SelfVisualisationThumbnailDrawable>()
        // )
      };
    }
  }

  [RelayCommand]
  private Task NavigateToDemo(string route)
  {
    _logger.LogInformation($"NavigateToDemo route: {route}");
    return _navigationService.GoToAsync(route);
  }

  public string Version
  {
    get => AppInfo.VersionString;
  }
}
