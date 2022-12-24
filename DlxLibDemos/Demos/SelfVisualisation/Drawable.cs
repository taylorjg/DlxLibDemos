namespace DlxLibDemos.Demos.SelfVisualisation;

public class SelfVisualisationDrawable : IDrawable
{
  private IWhatToDraw _whatToDraw;
  private float _width;
  private float _height;

  public SelfVisualisationDrawable(IWhatToDraw whatToDraw)
  {
    _whatToDraw = whatToDraw;
  }

  public void Draw(ICanvas canvas, RectF dirtyRect)
  {
    _width = dirtyRect.Width;
    _height = dirtyRect.Height;

    DrawBackground(canvas);
  }

  private void DrawBackground(ICanvas canvas)
  {
    canvas.FillColor = Colors.White;
    canvas.FillRectangle(0, 0, _width, _height);
  }
}
