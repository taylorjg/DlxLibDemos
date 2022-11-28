using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.NQueens;

public partial class NQueensDemoPageView : DemoPageBaseView
{
  private ILogger<NQueensDemoPageView> _logger;

  public NQueensDemoPageView(
    ILogger<NQueensDemoPageView> logger,
    NQueensDemoPageViewModel viewModel,
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
