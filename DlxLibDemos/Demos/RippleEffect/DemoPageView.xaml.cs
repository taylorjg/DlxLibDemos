using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.RippleEffect;

public partial class RippleEffectDemoPageView : DemoPageBaseView
{
  private ILogger<RippleEffectDemoPageView> _logger;

  public RippleEffectDemoPageView(
    ILogger<RippleEffectDemoPageView> logger,
    RippleEffectDemoPageViewModel viewModel,
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
