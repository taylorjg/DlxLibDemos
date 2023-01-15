using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.Crossword;

public partial class CrosswordDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<CrosswordDemoPageViewModel> _logger;

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
  }
}
