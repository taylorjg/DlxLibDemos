using Microsoft.Extensions.Logging;

namespace DlxLibDemos;

public partial class DemoPageBaseView : ContentPage
{
  private ILogger<DemoPageBaseView> _logger;
  private Grid _graphicsViewWrapper;
  private GraphicsView _graphicsView;

  public DemoPageBaseView(ILogger<DemoPageBaseView> logger, DemoPageBaseViewModel viewModel)
  {
    _logger = logger;
    _logger.LogInformation("constructor");
    InitializeComponent();
    BindingContext = viewModel;
    viewModel.NeedRedraw += (o, e) => OnNeedRedraw();
  }

  protected override void OnApplyTemplate()
  {
    base.OnApplyTemplate();
    _graphicsViewWrapper = (Grid)GetTemplateChild("graphicsViewWrapper");
    _graphicsView = (GraphicsView)GetTemplateChild("graphicsView");
    _graphicsView.SizeChanged += OnGraphicsViewSizeChanged;
  }

  private void OnGraphicsViewSizeChanged(object sender, EventArgs e)
  {
    var gvww = _graphicsViewWrapper.Width;
    var gvwh = _graphicsViewWrapper.Height;
    var gvsize = Math.Min(gvww, gvwh);
    _logger.LogInformation($"[OnGraphicsViewSizeChanged] {gvww}x{gvwh}, {gvsize}");
    _graphicsView.WidthRequest = gvsize;
    _graphicsView.HeightRequest = gvsize;
    OnNeedRedraw();
  }

  private void OnNeedRedraw()
  {
    _graphicsView?.Invalidate();
  }
}
