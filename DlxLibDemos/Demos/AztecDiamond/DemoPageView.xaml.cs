using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.AztecDiamond;

public partial class AztecDiamondDemoPageView : DemoPageBaseView
{
  private ILogger<AztecDiamondDemoPageView> _logger;

  public AztecDiamondDemoPageView(
    ILogger<AztecDiamondDemoPageView> logger,
    AztecDiamondDemoPageViewModel viewModel,
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
