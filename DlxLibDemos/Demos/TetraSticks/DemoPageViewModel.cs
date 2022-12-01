using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace DlxLibDemos.Demos.TetraSticks;

public partial class TetraSticksDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<TetraSticksDemoPageViewModel> _logger;

  public TetraSticksDemoPageViewModel(
    ILogger<TetraSticksDemoPageViewModel> logger,
    TetraSticksDemo demo,
    DemoPageBaseViewModel.Dependencies baseDependencies
  )
    : base(baseDependencies)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    Demo = demo;
  }
}
