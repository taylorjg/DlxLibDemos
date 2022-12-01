using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.TetraSticks;

public partial class TetraSticksDemoPageView : DemoPageBaseView
{
  private ILogger<TetraSticksDemoPageView> _logger;

  public TetraSticksDemoPageView(
    ILogger<TetraSticksDemoPageView> logger,
    TetraSticksDemoPageViewModel viewModel,
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
