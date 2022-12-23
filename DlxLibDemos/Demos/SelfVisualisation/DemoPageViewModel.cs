using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace DlxLibDemos.Demos.SelfVisualisation;

public partial class SelfVisualisationDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<SelfVisualisationDemoPageViewModel> _logger;

  public SelfVisualisationDemoPageViewModel(
    ILogger<SelfVisualisationDemoPageViewModel> logger,
    SelfVisualisationDemo demo,
    DemoPageBaseViewModel.Dependencies baseDependencies
  )
    : base(baseDependencies)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    Demo = demo;
  }
}
