using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace DlxLibDemos.Demos.DraughtboardPuzzle;

public partial class DraughtboardPuzzleDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<DraughtboardPuzzleDemoPageViewModel> _logger;
  private bool _showLabels;

  public DraughtboardPuzzleDemoPageViewModel(
    ILogger<DraughtboardPuzzleDemoPageViewModel> logger,
    DraughtboardPuzzleDemo demo,
    DemoPageBaseViewModel.Dependencies baseDependencies
  )
    : base(baseDependencies)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    Demo = demo;
    ShowLabels = false;
  }

  public bool ShowLabels
  {
    get => _showLabels;
    set
    {
      _logger.LogInformation($"ShowLabels setter value: {value}");
      SetProperty(ref _showLabels, value);
      DemoOptionalSettings = _showLabels;
    }
  }
}
