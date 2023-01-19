using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.Crossword;

public partial class CrosswordDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<CrosswordDemoPageViewModel> _logger;
  private bool _showClueNumbers;

  public CrosswordDemoPageViewModel(
    ILogger<CrosswordDemoPageViewModel> logger,
    CrosswordDemo demo,
    DemoPageBaseViewModel.Dependencies baseDependencies
  )
    : base(baseDependencies)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    Demo = demo;
    ShowClueNumbers = true;
  }

  public bool ShowClueNumbers
  {
    get => _showClueNumbers;
    set
    {
      _logger.LogInformation($"ShowClueNumbers setter value: {value}");
      SetProperty(ref _showClueNumbers, value);
      DemoDrawingOptions = _showClueNumbers;
    }
  }
}
