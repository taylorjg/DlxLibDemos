namespace DlxLibDemos.Demos.AztecDiamond;

public class AztecDiamondDrawable : IDrawable
{
  private IWhatToDraw _whatToDraw;
  private float _width;
  private float _height;
  private float _gridLineFullThickness;
  private float _gridLineHalfThickness;
  private float _polyLineFullThickness;
  private float _polyLineHalfThickness;
  private float _squareWidth;
  private float _squareHeight;
  private readonly Color _gridColour = Color.FromRgba("#80808080");

  private static readonly Dictionary<string, Color> PieceColours = new Dictionary<string, Color>
  {
    { "F", Color.FromRgba("#FF7366") },
    { "H", Color.FromRgba("#00E61A") },
    { "I", Color.FromRgba("#660066") },
    { "J", Color.FromRgba("#E6E6FF") },
    { "L", Color.FromRgba("#596673") },
    { "N", Color.FromRgba("#FFFF00") },
    { "O", Color.FromRgba("#CCCC1A") },
    { "P", Color.FromRgba("#994D33") },
    { "R", Color.FromRgba("#9926B2") },
    { "T", Color.FromRgba("#3300B2") },
    { "U", Color.FromRgba("#FF2699") },
    { "V", Color.FromRgba("#00FFFF") },
    { "W", Color.FromRgba("#CCFF00") },
    { "X", Color.FromRgba("#E60000") },
    { "Y", Color.FromRgba("#6659E6") },
    { "Z", Color.FromRgba("#008000") }
  };

  public AztecDiamondDrawable(IWhatToDraw whatToDraw)
  {
    _whatToDraw = whatToDraw;
  }

  public void Draw(ICanvas canvas, RectF dirtyRect)
  {
    _width = dirtyRect.Width;
    _height = dirtyRect.Height;
    _gridLineFullThickness = _width / 100;
    _gridLineHalfThickness = _gridLineFullThickness / 2;
    _polyLineFullThickness = _gridLineFullThickness * 2;
    _polyLineHalfThickness = _polyLineFullThickness / 2;
    _squareWidth = (_width - _polyLineFullThickness) / 9;
    _squareHeight = (_height - _polyLineFullThickness) / 9;

    DrawGrid(canvas);
    DrawPieces(canvas);
  }

  private void DrawGrid(ICanvas canvas)
  {
    canvas.SaveState();

    canvas.StrokeColor = _gridColour;
    canvas.StrokeSize = _gridLineFullThickness;
    canvas.StrokeLineCap = LineCap.Round;
    canvas.FillColor = Colors.Pink;

    DrawGridLines(canvas);

    canvas.RestoreState();
  }

  private void DrawGridLines(ICanvas canvas)
  {
    foreach (var horizontal in Locations.AllHorizontals)
    {
      DrawHorizontalGridLine(canvas, horizontal);
    }

    foreach (var vertical in Locations.AllVerticals)
    {
      DrawVerticalGridLine(canvas, vertical);
    }

    foreach (var junction in Locations.AllJunctions)
    {
      DrawJunction(canvas, junction);
    }
  }

  private void DrawHorizontalGridLine(ICanvas canvas, Coords coords)
  {
    var gap = _polyLineHalfThickness;
    var pt1 = CalculatePoint(coords);
    var pt2 = CalculatePoint(coords.Right());
    var x1 = pt1.X + gap;
    var x2 = pt2.X - gap;
    var y = pt1.Y;
    canvas.DrawLine(x1, y, x2, y);
  }

  private void DrawVerticalGridLine(ICanvas canvas, Coords coords)
  {
    var gap = _polyLineHalfThickness;
    var pt1 = CalculatePoint(coords);
    var pt2 = CalculatePoint(coords.Down());
    var x = pt1.X;
    var y1 = pt1.Y + gap;
    var y2 = pt2.Y - gap;
    canvas.DrawLine(x, y1, x, y2);
  }

  private void DrawJunction(ICanvas canvas, Coords coords)
  {
    var r = _gridLineHalfThickness / 2;
    var d = r * 2;
    var pt = CalculatePoint(coords);
    canvas.FillEllipse(pt.X - r, pt.Y - r, d, d);
  }

  private void DrawPieces(ICanvas canvas)
  {
    var solutionInternalRows = _whatToDraw.SolutionInternalRows.Cast<AztecDiamondInternalRow>();

    foreach (var internalRow in solutionInternalRows)
    {
      DrawPiece(canvas, internalRow);
    }
  }

  private void DrawPiece(ICanvas canvas, AztecDiamondInternalRow internalRow)
  {
    var colour = PieceColours[internalRow.Label];

    var location = internalRow.Location;
    var variation = internalRow.Variation;

    const int LINE_END_MULTIPLIER = 3;
    const int ROUNDED_CORNER_MULTIPLIER = 4;

    var insetPoint = (int multiplier) => (Coords coords1, Coords coords2, Point point) =>
    {
      var rowDiff = coords2.Row - coords1.Row;
      var colDiff = coords2.Col - coords1.Col;

      var verticalInset = _polyLineHalfThickness * multiplier;
      var horizontalInset = _polyLineHalfThickness * multiplier;

      return (rowDiff, colDiff) switch
      {
        (1, 0) => new Point(point.X, point.Y + verticalInset), // Down
        (-1, 0) => new Point(point.X, point.Y - verticalInset), // Up
        (0, 1) => new Point(point.X + horizontalInset, point.Y), // Right
        (0, -1) => new Point(point.X - horizontalInset, point.Y), // Left
        _ => point
      };
    };

    var insetLineStart = insetPoint(LINE_END_MULTIPLIER);
    var insetLineEnd = insetPoint(LINE_END_MULTIPLIER);
    var insetRoundedCornerStart = insetPoint(ROUNDED_CORNER_MULTIPLIER);
    var insetRoundedCornerEnd = insetPoint(ROUNDED_CORNER_MULTIPLIER);

    var roundedCornerTo = (Coords S, Coords V, Coords E, PathF path, PointF point) =>
    {
      // https://stackoverflow.com/a/40444735
      // const angle = ([a,b],[c,d],[e,f]) => (Math.atan2(f-d,e-c)-Math.atan2(b-d,a-c)+3*pi)%(2*pi)-pi;
      // const sweepFl = (S,V,E) => angle(E,S,V) > 0 ? 0 : 1;
      var angle = () =>
      {
        var (b, a) = E;
        var (d, c) = S;
        var (f, e) = V;
        return (Math.Atan2(f - d, e - c) - Math.Atan2(b - d, a - c) + 3 * Math.PI) % (2 * Math.PI) - Math.PI;
      };

      var r = _polyLineHalfThickness * ROUNDED_CORNER_MULTIPLIER;
      var largeArcFlag = false;
      var sweepFlag = angle() < 0;
      var x = point.X;
      var y = point.Y;
      var lastPointX = path.LastPoint.X;
      var lastPointY = path.LastPoint.Y;

      path.SVGArcTo(r, r, 0f, largeArcFlag, sweepFlag, x, y, lastPointX, lastPointY);
    };

    var paths = variation.PolyLines.Select(polyLine =>
    {
      var path = new PathF();

      if (polyLine[0] == polyLine[^1])
      {
        var roundedCornerStart = insetRoundedCornerStart(polyLine[0], polyLine[1], CalculatePoint(polyLine[0].Add(location)));
        path.MoveTo(roundedCornerStart);
      }
      else
      {
        var lineStart = insetLineStart(polyLine[0], polyLine[1], CalculatePoint(polyLine[0].Add(location)));
        path.MoveTo(lineStart);
      }

      var indices = Enumerable.Range(0, polyLine.Length).ToArray();

      foreach (var index in indices[1..^1])
      {
        var coords = polyLine[index];
        var coordsPrev = polyLine[index - 1];
        var coordsNext = polyLine[index + 1];
        var point = CalculatePoint(coords.Add(location));

        if (coordsPrev.Row == coordsNext.Row || coordsPrev.Col == coordsNext.Col)
        {
          path.LineTo(point);
        }
        else
        {
          var roundedCornerStart = insetRoundedCornerStart(coords, coordsPrev, point);
          path.LineTo(roundedCornerStart);

          var roundedCornerEnd = insetRoundedCornerEnd(coords, coordsNext, point);
          roundedCornerTo(coordsPrev, coords, coordsNext, path, roundedCornerEnd);
        }
      }

      if (polyLine[0] == polyLine[^1])
      {
        var coords = polyLine[0];
        var coordsPrev = polyLine[^2]; // not [^1] because [^1] is the same coords as [0]
        var coordsNext = polyLine[1];
        var point = CalculatePoint(coords.Add(location));

        var roundedCornerStart = insetRoundedCornerStart(coords, coordsPrev, point);
        path.LineTo(roundedCornerStart);

        roundedCornerTo(coordsPrev, coords, coordsNext, path, path.FirstPoint);
      }
      else
      {
        var lineEnd = insetLineEnd(polyLine[^1], polyLine[^2], CalculatePoint(polyLine[^1].Add(location)));
        path.LineTo(lineEnd);
      }

      return path;
    }).ToList();

    paths.ForEach(path =>
    {
      canvas.StrokeColor = Colors.Black;
      canvas.StrokeSize = _polyLineFullThickness;
      canvas.StrokeLineCap = LineCap.Round;
      canvas.DrawPath(path);
    });

    paths.ForEach(path =>
    {
      canvas.StrokeColor = colour;
      canvas.StrokeSize = _polyLineFullThickness * 0.75f;
      canvas.StrokeLineCap = LineCap.Round;
      canvas.DrawPath(path);
    });
  }

  private float CalculateX(int col) => col * _squareWidth + _polyLineHalfThickness;
  private float CalculateY(int row) => row * _squareHeight + _polyLineHalfThickness;

  private PointF CalculatePoint(Coords coords) =>
    new PointF(CalculateX(coords.Col), CalculateY(coords.Row));
}
