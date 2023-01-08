using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.Kakuro;

public partial class KakuroDemoPageView : DemoPageBaseView
{
  private ILogger<KakuroDemoPageView> _logger;

  public KakuroDemoPageView(
    ILogger<KakuroDemoPageView> logger,
    KakuroDemoPageViewModel viewModel,
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
