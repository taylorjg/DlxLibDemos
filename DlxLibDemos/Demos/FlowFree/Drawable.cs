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
    DrawPipes(canvas);
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
    var colour = GetColorForColorPair(colourPair);

    DrawDot(canvas, colour, colourPair.Start);
    DrawDot(canvas, colour, colourPair.End);
  }

  private void DrawDot(ICanvas canvas, Color colour, Coords coords)
  {
    var width = _squareWidth * 0.75f;
    var height = _squareHeight * 0.75f;
    var squareCentre = CalculateSquareCentre(coords);
    var x = squareCentre.X - width / 2;
    var y = squareCentre.Y - height / 2;

    canvas.FillColor = colour;
    canvas.FillEllipse(x, y, width, height);
  }

  private void DrawPipes(ICanvas canvas)
  {
    var solutionInternalRows = _whatToDraw.SolutionInternalRows.Cast<FlowFreeInternalRow>();

    foreach (var internalRow in solutionInternalRows)
    {
      var colour = GetColorForColorPair(internalRow.ColourPair);
      var pipe = internalRow.Pipe;
      DrawPipe(canvas, colour, pipe);
    }
  }

  private void DrawPipe(ICanvas canvas, Color colour, Coords[] pipe)
  {
    var path = new PathF();

    path.MoveTo(CalculateSquareCentre(pipe[0]));

    foreach (var coords in pipe.Skip(1))
    {
      path.LineTo(CalculateSquareCentre(coords));
    }

    canvas.StrokeColor = colour;
    canvas.StrokeSize = _squareWidth / 3;
    canvas.StrokeLineCap = LineCap.Round;
    canvas.StrokeLineJoin = LineJoin.Round;
    canvas.DrawPath(path);
  }

  private Color GetColorForColorPair(ColourPair colourPair)
  {
    return DotColours.GetValueOrDefault(colourPair.Label) ?? Colors.White;
  }

  private float CalculateX(int col) => col * _squareWidth + _gridLineHalfThickness;
  private float CalculateY(int row) => row * _squareHeight + _gridLineHalfThickness;

  private PointF CalculatePoint(Coords coords) =>
    new PointF(CalculateX(coords.Col), CalculateY(coords.Row));

  private PointF CalculateSquareCentre(Coords coords) =>
    new PointF(
      CalculateX(coords.Col) + _squareWidth / 2,
      CalculateY(coords.Row) + _squareHeight / 2
    );
}
