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

namespace DlxLibDemos;

public partial class HomePageViewModel : ObservableObject
{
  private ILogger<HomePageViewModel> _logger;
  private INavigationService _navigationService;
  private IDemoConfig[] _availableDemos;

  public HomePageViewModel(
    ILogger<HomePageViewModel> logger,
    INavigationService navigationService,
    SudokuDemoConfig sudokuDemoConfig,
    PentominoesDemoConfig pentominoesDemoConfig,
    NQueensDemoConfig nQueensDemoConfig,
    DraughtboardPuzzleDemoConfig draughtboardPuzzleDemoConfig,
    TetraSticksDemoConfig tetraSticksDemoConfig,
    AztecDiamondDemoConfig aztecDiamondDemoConfig,
    RippleEffectDemoConfig rippleEffectDemoConfig,
    FlowFreeDemoConfig flowFreeDemoConfig,
    KakuroDemoConfig kakuroDemoConfig,
    NonogramDemoConfig nonogramDemoConfig,
    CrosswordDemoConfig crosswordDemoConfig
  )
  {
    _logger = logger;
    _navigationService = navigationService;
    _availableDemos = new IDemoConfig[] {
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
      crosswordDemoConfig
    };
    _logger.LogInformation("constructor");
  }

  public IDemoConfig[] AvailableDemos { get => _availableDemos; }

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
