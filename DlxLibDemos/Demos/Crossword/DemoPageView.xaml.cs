using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.Crossword;

public partial class CrosswordDemoPageView : DemoPageBaseView
{
  private ILogger<CrosswordDemoPageView> _logger;

  public CrosswordDemoPageView(
    ILogger<CrosswordDemoPageView> logger,
    CrosswordDemoPageViewModel viewModel,
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
