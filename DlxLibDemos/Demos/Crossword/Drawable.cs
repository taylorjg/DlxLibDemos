namespace DlxLibDemos.Demos.Crossword;

public class CrosswordDrawable : IDrawable
{
  private IWhatToDraw _whatToDraw;
  private float _width;
  private float _height;
  private float _squareWidth;
  private float _squareHeight;
  private float _gridLineFullThickness;
  private float _gridLineHalfThickness;

  public CrosswordDrawable(IWhatToDraw whatToDraw)
  {
    _whatToDraw = whatToDraw;
  }

  public void Draw(ICanvas canvas, RectF dirtyRect)
  {
    _width = dirtyRect.Width;
    _height = dirtyRect.Height;
    _gridLineFullThickness = _width / 400;
    _gridLineHalfThickness = _gridLineFullThickness / 2;
    _squareWidth = (_width - _gridLineFullThickness) / 10;
    _squareHeight = (_height - _gridLineFullThickness) / 10;

    DrawBackground(canvas);
    DrawGrid(canvas);
  }

  private void DrawBackground(ICanvas canvas)
  {
    var x = _gridLineHalfThickness;
    var y = _gridLineHalfThickness;
    var w = _width - _gridLineFullThickness;
    var h = _height - _gridLineFullThickness;

    canvas.FillColor = Colors.White;
    canvas.FillRectangle(x, y, w, h);
  }

  private void DrawGrid(ICanvas canvas)
  {
    canvas.SaveState();
    canvas.BlendMode = BlendMode.Copy;
    DrawHorizontalGridLines(canvas);
    DrawVerticalGridLines(canvas);
    canvas.RestoreState();
  }

  private void DrawHorizontalGridLines(ICanvas canvas)
  {
    foreach (var row in Enumerable.Range(0, 10 + 1))
    {
      var x1 = _gridLineHalfThickness;
      var x2 = _width - _gridLineHalfThickness;
      var y = CalculateY(row);
      canvas.StrokeColor = Colors.Black;
      canvas.StrokeSize = _gridLineFullThickness;
      canvas.DrawLine(x1, y, x2, y);
    }
  }

  private void DrawVerticalGridLines(ICanvas canvas)
  {
    foreach (var col in Enumerable.Range(0, 10 + 1))
    {
      var x = CalculateX(col);
      var y1 = _gridLineHalfThickness;
      var y2 = _height - _gridLineHalfThickness;
      canvas.StrokeColor = Colors.Black;
      canvas.StrokeSize = _gridLineFullThickness;
      canvas.DrawLine(x, y1, x, y2);
    }
  }

  private float CalculateX(int col) => col * _squareWidth + _gridLineHalfThickness;
  private float CalculateY(int row) => row * _squareHeight + _gridLineHalfThickness;

  private PointF CalculatePoint(Coords coords) =>
    new PointF(CalculateX(coords.Col), CalculateY(coords.Row));
}
