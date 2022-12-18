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

  private static readonly Dictionary<string, Color> DotColours = new Dictionary<string, Color>
  {
    { "A", Colors.Red },
    { "B", Colors.Green },
    { "C", Colors.Blue },
    { "D", Colors.Yellow },
    { "E", Colors.Orange }
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
    DrawColourPairs(canvas);
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

  private void DrawColourPairs(ICanvas canvas)
  {
    foreach (var colourPair in _puzzle.ColourPairs)
    {
      DrawColourPair(canvas, colourPair);
    }
  }

  private void DrawColourPair(ICanvas canvas, ColourPair colourPair)
  {
    var colour = DotColours.GetValueOrDefault(colourPair.Label) ?? Colors.White;

    DrawDot(canvas, colour, colourPair.Start);
    DrawDot(canvas, colour, colourPair.End);
  }

  private void DrawDot(ICanvas canvas, Color colour, Coords coords)
  {
    var w = _squareWidth * 0.75f;
    var h = _squareHeight * 0.75f;
    var x = CalculateX(coords.Col) + _squareWidth / 2 - w / 2;
    var y = CalculateY(coords.Row) + _squareHeight / 2 - h / 2;

    canvas.FillColor = colour;
    canvas.FillEllipse(x, y, w, h);
  }

  private float CalculateX(int col) => col * _squareWidth + _gridLineHalfThickness;
  private float CalculateY(int row) => row * _squareHeight + _gridLineHalfThickness;

  private PointF CalculatePoint(Coords coords) =>
    new PointF(CalculateX(coords.Col), CalculateY(coords.Row));
}
