using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.SelfVisualisation;

public partial class SelfVisualisationDemoPageView : DemoPageBaseView
{
  private ILogger<SelfVisualisationDemoPageView> _logger;

  public SelfVisualisationDemoPageView(
    ILogger<SelfVisualisationDemoPageView> logger,
    SelfVisualisationDemoPageViewModel viewModel,
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
