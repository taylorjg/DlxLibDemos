namespace DlxLibDemos.Demos.TetraSticks;

public class TetraSticksDrawable : IDrawable
{
  private IWhatToDraw _whatToDraw;
  private float _width;
  private float _height;
  private float _gridLineFullThickness;
  private float _gridLineHalfThickness;
  private float _squareWidth;
  private float _squareHeight;
  private readonly Color _gridColour = Color.FromRgba("#CD853F80");

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

  public TetraSticksDrawable(IWhatToDraw whatToDraw)
  {
    _whatToDraw = whatToDraw;
  }

  public void Draw(ICanvas canvas, RectF dirtyRect)
  {
    _width = dirtyRect.Width;
    _height = dirtyRect.Height;
    _gridLineFullThickness = dirtyRect.Width / 100;
    _gridLineHalfThickness = _gridLineFullThickness / 2;
    _squareWidth = (dirtyRect.Width - _gridLineFullThickness) / 5;
    _squareHeight = (dirtyRect.Height - _gridLineFullThickness) / 5;

    DrawGrid(canvas);
    DrawPieces(canvas);
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
    foreach (var row in Enumerable.Range(0, 6))
    {
      var x1 = 0;
      var y1 = CalculateY(row);
      var x2 = _width;
      var y2 = y1;
      canvas.StrokeColor = _gridColour;
      canvas.StrokeSize = _gridLineFullThickness;
      canvas.DrawLine(x1, y1, x2, y2);
    }
  }

  private void DrawVerticalGridLines(ICanvas canvas)
  {
    foreach (var col in Enumerable.Range(0, 6))
    {
      var x1 = CalculateX(col);
      var y1 = 0;
      var x2 = x1;
      var y2 = _height;
      canvas.StrokeColor = _gridColour;
      canvas.StrokeSize = _gridLineFullThickness;
      canvas.DrawLine(x1, y1, x2, y2);
    }
  }

  private void DrawPieces(ICanvas canvas)
  {
    var solutionInternalRows = _whatToDraw.SolutionInternalRows.Cast<TetraSticksInternalRow>();

    foreach (var internalRow in solutionInternalRows)
    {
      DrawPiece(canvas, internalRow);
    }
  }

  // private void DrawPiece(ICanvas canvas, TetraSticksInternalRow internalRow)
  // {
  //   var colour = PieceColours[internalRow.Label];

  //   var location = internalRow.Location;
  //   var variation = internalRow.Variation;

  //   var horizontalPaths = variation.Horizontals.Select(horizontal =>
  //   {
  //     var x1 = CalculateX(location.Col + horizontal.Col) + _squareWidth * 0.1f;
  //     var x2 = CalculateX(location.Col + horizontal.Col + 1) - _squareWidth * 0.1f;
  //     var y = CalculateY(location.Row + horizontal.Row);
  //     var path = new PathF();
  //     path.MoveTo(x1, y);
  //     path.LineTo(x2, y);
  //     return path;
  //   }).ToList();

  //   horizontalPaths.ForEach(path =>
  //   {
  //     canvas.StrokeColor = Colors.Black;
  //     canvas.StrokeSize = _squareWidth * 0.1f;
  //     canvas.StrokeLineJoin = LineJoin.Round;
  //     canvas.StrokeLineCap = LineCap.Round;
  //     canvas.DrawPath(path);
  //   });

  //   horizontalPaths.ForEach(path =>
  //   {
  //     canvas.StrokeColor = colour;
  //     canvas.StrokeSize = _squareWidth * 0.08f;
  //     canvas.StrokeLineJoin = LineJoin.Round;
  //     canvas.StrokeLineCap = LineCap.Round;
  //     canvas.DrawPath(path);
  //   });

  //   var verticalPaths = variation.Verticals.Select(vertical =>
  //   {
  //     var x = CalculateX(location.Col + vertical.Col);
  //     var y1 = CalculateY(location.Row + vertical.Row) + _squareHeight * 0.1f;
  //     var y2 = CalculateY(location.Row + vertical.Row + 1) - _squareHeight * 0.1f;
  //     var path = new PathF();
  //     path.MoveTo(x, y1);
  //     path.LineTo(x, y2);
  //     return path;
  //   }).ToList();

  //   verticalPaths.ForEach(path =>
  //   {
  //     canvas.StrokeColor = Colors.Black;
  //     canvas.StrokeSize = _squareHeight * 0.1f;
  //     canvas.StrokeLineJoin = LineJoin.Round;
  //     canvas.StrokeLineCap = LineCap.Round;
  //     canvas.DrawPath(path);
  //   });

  //   verticalPaths.ForEach(path =>
  //   {
  //     canvas.StrokeColor = colour;
  //     canvas.StrokeSize = _squareHeight * 0.08f;
  //     canvas.StrokeLineJoin = LineJoin.Round;
  //     canvas.StrokeLineCap = LineCap.Round;
  //     canvas.DrawPath(path);
  //   });

  //   // foreach (var junction in variation.Junctions)
  //   // {
  //   //   var cx = CalculateX(location.Col + junction.Col);
  //   //   var cy = CalculateY(location.Row + junction.Row);
  //   //   var r = _squareWidth * 0.02f;
  //   //   canvas.FillColor = Colors.Magenta;
  //   //   canvas.FillCircle(cx, cy, r);
  //   // }
  // }

  private void DrawPiece(ICanvas canvas, TetraSticksInternalRow internalRow)
  {
    var colour = PieceColours[internalRow.Label];

    var location = internalRow.Location;
    var variation = internalRow.Variation;

    var addLocation = (Coords coords) =>
      new Coords(
        location.Row + coords.Row,
        location.Col + coords.Col
      );

    var insetLineEnding = (Coords coords1, Coords coords2, Point point) =>
    {
      var rowDiff = coords2.Row - coords1.Row;
      var colDiff = coords2.Col - coords1.Col;

      var verticalInset = _squareHeight * 0.15f;
      var horizontalInset = _squareWidth * 0.15f;

      return (rowDiff, colDiff) switch
      {
        (1, 0) => new Point(point.X, point.Y + verticalInset), // Down
        (-1, 0) => new Point(point.X, point.Y - verticalInset), // Up
        (0, 1) => new Point(point.X + horizontalInset, point.Y), // Right
        (0, -1) => new Point(point.X - horizontalInset, point.Y), // Left
        _ => point
      };
    };

    var paths = variation.PolyLines.Select(polyLine =>
    {
      var path = new PathF();

      path.MoveTo(insetLineEnding(polyLine[0], polyLine[1], CalculatePoint(addLocation(polyLine.First()))));

      foreach (var coords in polyLine[1..^1])
      {
        path.LineTo(CalculatePoint(addLocation(coords)));
      }

      path.LineTo(insetLineEnding(polyLine[^1], polyLine[^2], CalculatePoint(addLocation(polyLine.Last()))));

      return path;
    }).ToList();

    paths.ForEach(path =>
    {
      canvas.StrokeColor = Colors.Black;
      canvas.StrokeSize = _squareHeight * 0.1f;
      canvas.StrokeLineJoin = LineJoin.Round;
      canvas.StrokeLineCap = LineCap.Round;
      canvas.DrawPath(path);
    });

    paths.ForEach(path =>
    {
      canvas.StrokeColor = colour;
      canvas.StrokeSize = _squareHeight * 0.08f;
      canvas.StrokeLineJoin = LineJoin.Round;
      canvas.StrokeLineCap = LineCap.Round;
      canvas.DrawPath(path);
    });
  }

  private float CalculateX(int col) => col * _squareWidth + _gridLineHalfThickness;
  private float CalculateY(int row) => row * _squareHeight + _gridLineHalfThickness;

  private PointF CalculatePoint(Coords coords) =>
    new PointF(CalculateX(coords.Col), CalculateY(coords.Row));
}
