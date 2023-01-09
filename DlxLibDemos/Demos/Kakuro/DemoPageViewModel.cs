using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.Kakuro;

public partial class KakuroDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<KakuroDemoPageViewModel> _logger;
  private bool _showLabels;

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
    ShowLabels = true;
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
