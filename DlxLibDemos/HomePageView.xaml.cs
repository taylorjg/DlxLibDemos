using Microsoft.Extensions.Logging;

namespace DlxLibDemos;

public partial class HomePageView : ContentPage
{
  private ILogger<HomePageView> _logger;
  private HomePageViewModel _viewModel;

  public HomePageView(ILogger<HomePageView> logger, HomePageViewModel viewModel)
  {
    _logger = logger;
    _viewModel = viewModel;
    _logger.LogInformation("constructor");
    InitializeComponent();
    BindingContext = viewModel;
    SizeChanged += OnSizeChanged;
  }

  private void OnSizeChanged(object sender, EventArgs e)
  {
    const int TILE_WIDTH = 400;
    const int TILE_HEIGHT = 220;
    const int H_GAP = 10;
    const int V_GAP = 10;

    var tileCount = _viewModel.AvailableDemos.Length;

    var numTilesWide = (int)Math.Floor(Width / (TILE_WIDTH + H_GAP));
    var numTilesHigh = (int)Math.Ceiling((double)tileCount / numTilesWide);
    _logger.LogInformation($"[OnGraphicsViewSizeChanged] {new { numTilesWide, numTilesHigh }}");

    var approxFlexLayoutWidth = numTilesWide * TILE_WIDTH + (numTilesWide * 2) * H_GAP;
    var approxFlexLayoutHeight = numTilesHigh * TILE_HEIGHT + (numTilesHigh * 2) * V_GAP;
    _logger.LogInformation($"[OnGraphicsViewSizeChanged] {new { approxFlexLayoutWidth, approxFlexLayoutHeight }}");

    TilesScrollView.WidthRequest = approxFlexLayoutWidth;
    TilesFlexLayout.WidthRequest = approxFlexLayoutWidth;
    TilesFlexLayout.HeightRequest = approxFlexLayoutHeight;
  }
}
