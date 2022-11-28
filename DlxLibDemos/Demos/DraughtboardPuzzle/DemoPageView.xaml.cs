using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.DraughtboardPuzzle;

public partial class DraughtboardPuzzleDemoPageView : DemoPageBaseView
{
  private ILogger<DraughtboardPuzzleDemoPageView> _logger;

  public DraughtboardPuzzleDemoPageView(
    ILogger<DraughtboardPuzzleDemoPageView> logger,
    DraughtboardPuzzleDemoPageViewModel viewModel,
    ILogger<DemoPageBaseView> loggerBase
  )
    : base(loggerBase, viewModel)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    InitializeComponent();
    BindingContext = viewModel;
  }
}
