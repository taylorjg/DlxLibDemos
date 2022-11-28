using Microsoft.Extensions.Logging;

namespace DlxLibDemos;

public partial class HomePageView : ContentPage
{
  private ILogger<HomePageView> _logger;

  public HomePageView(ILogger<HomePageView> logger, HomePageViewModel viewModel)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    InitializeComponent();
    BindingContext = viewModel;
  }
}
