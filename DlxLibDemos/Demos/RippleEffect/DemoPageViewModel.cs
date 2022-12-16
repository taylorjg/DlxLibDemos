using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace DlxLibDemos.Demos.RippleEffect;

public partial class RippleEffectDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<RippleEffectDemoPageViewModel> _logger;

  public RippleEffectDemoPageViewModel(
    ILogger<RippleEffectDemoPageViewModel> logger,
    RippleEffectDemo demo,
    DemoPageBaseViewModel.Dependencies baseDependencies
  )
    : base(baseDependencies)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    Demo = demo;
  }
}
