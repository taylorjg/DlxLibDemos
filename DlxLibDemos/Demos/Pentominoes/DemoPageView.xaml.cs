using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.Pentominoes;

public partial class PentominoesDemoPageView : DemoPageBaseView
{
  private ILogger<PentominoesDemoPageView> _logger;

  public PentominoesDemoPageView(
    ILogger<PentominoesDemoPageView> logger,
    PentominoesDemoPageViewModel viewModel,
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
