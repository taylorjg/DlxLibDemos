using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.Nonogram;

public partial class NonogramDemoPageViewModel : DemoPageBaseViewModel
{
  private ILogger<NonogramDemoPageViewModel> _logger;

  public NonogramDemoPageViewModel(
    ILogger<NonogramDemoPageViewModel> logger,
    NonogramDemo demo,
    DemoPageBaseViewModel.Dependencies baseDependencies
  )
    : base(baseDependencies)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    Demo = demo;

    var cancellationTokenSource = new CancellationTokenSource();
    demo.BuildInternalRows(null, cancellationTokenSource.Token);
  }
}
