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
    MyScrollView.SizeChanged += OnMyScrollViewSizeChanged;
  }

  private void OnMyScrollViewSizeChanged(object sender, EventArgs e)
  {
    var scrollViewWidth = MyScrollView.Width;
    var scrollViewHeight = MyScrollView.Height;
    _logger.LogInformation($"[OnGraphicsViewSizeChanged] scrollViewWidth {scrollViewWidth}; scrollViewHeight: {scrollViewHeight}");

    const int TILE_WIDTH = 400;
    const int MINIMUM_TILE_HEIGHT = 150;
    const int GAP = 10;

    var availableDemoCount = _viewModel.AvailableDemos.Length;

    var numTilesWide = Math.Max((int)(scrollViewWidth / TILE_WIDTH), 1);
    var numTilesHigh = Math.Max((int)(availableDemoCount / numTilesWide), 1);
    _logger.LogInformation($"[OnGraphicsViewSizeChanged] numTilesWide {numTilesWide}; numTilesHigh: {numTilesHigh}");

    var approxFlexLayoutHeight = numTilesHigh * MINIMUM_TILE_HEIGHT + (numTilesHigh - 1) * GAP;
    _logger.LogInformation($"[OnGraphicsViewSizeChanged] approxFlexLayoutHeight {approxFlexLayoutHeight}");
    MyFlexLayout.HeightRequest = Math.Max(approxFlexLayoutHeight, scrollViewHeight);
  }
}
