using Microsoft.Extensions.Logging;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MetroLog.Maui;

namespace DlxLibDemos.Demos.AztecDiamond;

public partial class AztecDiamondDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<AztecDiamondDemoPageViewModel> _logger;

  public AztecDiamondDemoPageViewModel(
    ILogger<AztecDiamondDemoPageViewModel> logger,
    AztecDiamondDemo demo,
    DemoPageBaseViewModel.Dependencies baseDependencies
  )
    : base(baseDependencies)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    Demo = demo;
  }
}
