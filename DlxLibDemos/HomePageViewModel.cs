using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;
using System.Windows.Input;
using DlxLibDemos.Demos.Sudoku;
using DlxLibDemos.Demos.Pentominoes;
using DlxLibDemos.Demos.NQueens;
using DlxLibDemos.Demos.DraughtboardPuzzle;

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
    _logger.LogInformation($"FileSystem.CacheDirectory: {FileSystem.CacheDirectory}");
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
        )
      };
    }
  }

  [RelayCommand]
  private Task NavigateToDemo(string route)
  {
    _logger.LogInformation($"NavigateToDemo route: {route}");
    return _navigationService.GoToAsync(route);
  }
}
