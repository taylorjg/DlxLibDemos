using Microsoft.Extensions.Logging;

namespace DlxLibDemos.Demos.Sudoku;

public partial class SudokuDemoPageView : DemoPageBaseView
{
  private ILogger<SudokuDemoPageView> _logger;

  public SudokuDemoPageView(
    ILogger<SudokuDemoPageView> logger,
    SudokuDemoPageViewModel viewModel,
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
