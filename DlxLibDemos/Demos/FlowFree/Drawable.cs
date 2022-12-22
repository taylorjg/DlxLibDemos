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
    { "E", Colors.Orange },
    { "F", Colors.Cyan },
    { "G", Colors.Magenta },
    { "H", Colors.Brown },
    { "I", Colors.Purple },
    { "J", Colors.White },
    { "K", Colors.Grey },
    { "L", Colors.LimeGreen }
  };

  private static readonly Dictionary<string, Color> LabelColours = new Dictionary<string, Color>
  {
    { "A", Colors.White },
    { "B", Colors.White },
    { "C", Colors.White },
    { "D", Colors.Black },
    { "E", Colors.Black },
    { "F", Colors.Black },
    { "G", Colors.White },
    { "H", Colors.White },
    { "I", Colors.White },
    { "J", Colors.Black },
    { "K", Colors.Black },
    { "L", Colors.Black }
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
    _gridLineFullThickness = _width / 600;
    _gridLineHalfThickness = _gridLineFullThickness / 2;
    _squareWidth = (_width - _gridLineFullThickness) / size;
    _squareHeight = (_height - _gridLineFullThickness) / size;

    DrawBackground(canvas);
    DrawGrid(canvas);
    DrawColourPairDots(canvas);
    DrawPipes(canvas);
    DrawColourPairLabels(canvas);
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

  private void DrawColourPairDots(ICanvas canvas)
  {
    foreach (var colourPair in _puzzle.ColourPairs)
    {
      DrawDot(canvas, colourPair.Label, colourPair.Start);
      DrawDot(canvas, colourPair.Label, colourPair.End);
    }
  }

  private void DrawDot(ICanvas canvas, string label, Coords coords)
  {
    var colour = GetDotColor(label);
    var width = _squareWidth * 0.7f;
    var height = _squareHeight * 0.7f;
    var squareCentre = CalculateSquareCentre(coords);
    var x = squareCentre.X - width / 2;
    var y = squareCentre.Y - height / 2;

    canvas.FillColor = colour;
    canvas.FillEllipse(x, y, width, height);
  }

  private void DrawColourPairLabels(ICanvas canvas)
  {
    foreach (var colourPair in _puzzle.ColourPairs)
    {
      DrawLabel(canvas, colourPair.Label, colourPair.Start);
      DrawLabel(canvas, colourPair.Label, colourPair.End);
    }
  }

  private void DrawLabel(ICanvas canvas, string label, Coords coords)
  {
    var colour = GetLabelColor(label);
    var width = _squareWidth * 0.7f;
    var height = _squareHeight * 0.7f;
    var squareCentre = CalculateSquareCentre(coords);
    var x = squareCentre.X - width / 2;
    var y = squareCentre.Y - height / 2;

    canvas.FontColor = colour;
    canvas.FontSize = _squareWidth * 0.5f;
    canvas.DrawString(
      label,
      x,
      y,
      width,
      height,
      HorizontalAlignment.Center,
      VerticalAlignment.Center
    );
  }

  private void DrawPipes(ICanvas canvas)
  {
    var solutionInternalRows = _whatToDraw.SolutionInternalRows.Cast<FlowFreeInternalRow>();

    foreach (var internalRow in solutionInternalRows)
    {
      var colour = GetDotColor(internalRow.ColourPair.Label);
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

  private Color GetDotColor(string label)
  {
    return DotColours.GetValueOrDefault(label) ?? Colors.White;
  }

  private Color GetLabelColor(string label)
  {
    return LabelColours.GetValueOrDefault(label) ?? Colors.White;
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
