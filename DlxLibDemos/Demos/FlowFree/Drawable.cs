namespace DlxLibDemos.Demos.FlowFree;

public class FlowFreeDrawable : IDrawable
{
  private IWhatToDraw _whatToDraw;
  private float _width;
  private float _height;
  private float _gridLineFullThickness;
  private float _gridLineHalfThickness;
  private float _squareWidth;
  private float _squareHeight;
  private readonly Color _gridColour = Colors.Yellow;
  private Puzzle _puzzle;

  private static readonly Dictionary<string, Color> PieceColours = new Dictionary<string, Color>
  {
    { "F", Color.FromRgba("#CCCCE5") },
    { "I", Color.FromRgba("#650205") },
    { "L", Color.FromRgba("#984D11") },
    { "N", Color.FromRgba("#FFFD38") },
    { "P", Color.FromRgba("#FD8023") },
    { "T", Color.FromRgba("#FC2028") },
    { "U", Color.FromRgba("#7F1CC9") },
    { "V", Color.FromRgba("#6783E3") },
    { "W", Color.FromRgba("#0F7F12") },
    { "X", Color.FromRgba("#FC1681") },
    { "Y", Color.FromRgba("#29FD2F") },
    { "Z", Color.FromRgba("#CCCA2A") }
  };

  public FlowFreeDrawable(IWhatToDraw whatToDraw)
  {
    _whatToDraw = whatToDraw;
  }

  public void Draw(ICanvas canvas, RectF dirtyRect)
  {
    _puzzle = (Puzzle)_whatToDraw.DemoSettings;
    var size = _puzzle.Size;

    _width = dirtyRect.Width;
    _height = dirtyRect.Height;
    _gridLineFullThickness = _width / 400;
    _gridLineHalfThickness = _gridLineFullThickness / 2;
    _squareWidth = (_width - _gridLineFullThickness) / size;
    _squareHeight = (_height - _gridLineFullThickness) / size;

    DrawBackground(canvas);
    DrawGrid(canvas);
  }

  private void DrawBackground(ICanvas canvas)
  {
    var x = 0;
    var y = 0;
    var w = _width;
    var h = _height;

    canvas.FillColor = Colors.Black;
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
    foreach (var row in Enumerable.Range(0, _puzzle.Size + 1))
    {
      var x1 = 0;
      var x2 = _width;
      var y = CalculateY(row);
      canvas.StrokeColor = _gridColour;
      canvas.StrokeSize = _gridLineFullThickness;
      canvas.DrawLine(x1, y, x2, y);
    }
  }

  private void DrawVerticalGridLines(ICanvas canvas)
  {
    foreach (var col in Enumerable.Range(0, _puzzle.Size + 1))
    {
      var x = CalculateX(col);
      var y1 = 0;
      var y2 = _height;
      canvas.StrokeColor = _gridColour;
      canvas.StrokeSize = _gridLineFullThickness;
      canvas.DrawLine(x, y1, x, y2);
    }
  }

  private float CalculateX(int col) => col * _squareWidth + _gridLineHalfThickness;
  private float CalculateY(int row) => row * _squareHeight + _gridLineHalfThickness;

  private PointF CalculatePoint(Coords coords) =>
    new PointF(CalculateX(coords.Col), CalculateY(coords.Row));
}
