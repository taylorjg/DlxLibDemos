using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.Kakuro;

public partial class KakuroDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<KakuroDemoPageViewModel> _logger;
  private bool _showClues;

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
    ShowClues = true;
  }

  public bool ShowClues
  {
    get => _showClues;
    set
    {
      _logger.LogInformation($"ShowClues setter value: {value}");
      SetProperty(ref _showClues, value);
      DemoDrawingOptions = _showClues;
    }
  }
}
