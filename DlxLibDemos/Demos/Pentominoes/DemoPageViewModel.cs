using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace DlxLibDemos.Demos.Pentominoes;

public partial class PentominoesDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<PentominoesDemoPageViewModel> _logger;
  private bool _showLabels;

  public PentominoesDemoPageViewModel(
    ILogger<PentominoesDemoPageViewModel> logger,
    PentominoesDemo demo,
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
