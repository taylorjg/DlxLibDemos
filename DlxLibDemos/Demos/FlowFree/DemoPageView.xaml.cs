using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.FlowFree;

public partial class FlowFreeDemoPageView : DemoPageBaseView
{
  private ILogger<FlowFreeDemoPageView> _logger;

  public FlowFreeDemoPageView(
    ILogger<FlowFreeDemoPageView> logger,
    FlowFreeDemoPageViewModel viewModel,
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
