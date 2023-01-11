using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.Nonogram;

public partial class NonogramDemoPageView : DemoPageBaseView
{
  private ILogger<NonogramDemoPageView> _logger;

  public NonogramDemoPageView(
    ILogger<NonogramDemoPageView> logger,
    NonogramDemoPageViewModel viewModel,
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
