using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.Kakuro;

public partial class KakuroDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<KakuroDemoPageViewModel> _logger;

  public KakuroDemoPageViewModel(
    ILogger<KakuroDemoPageViewModel> logger,
    KakuroDemo demo,
    DemoPageBaseViewModel.Dependencies baseDependencies
  )
    : base(baseDependencies)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    Demo = demo;
  }
}
