namespace DlxLibDemos.Demos.DraughtboardPuzzle;

public class DraughtboardPuzzleDrawable : IDrawable
{
  private IWhatToDraw _whatToDraw;
  private float _width;
  private float _height;
  private float _gridLineFullThickness;
  private float _gridLineHalfThickness;
  private float _squareWidth;
  private float _squareHeight;
  private readonly Color _gridColour = Color.FromRgba("#CD853F80");
  private readonly Color _borderColour = Color.FromRgba("#0066CC");

  public DraughtboardPuzzleDrawable(IWhatToDraw whatToDraw)
  {
    _whatToDraw = whatToDraw;
  }

  public void Draw(ICanvas canvas, RectF dirtyRect)
  {
    _width = dirtyRect.Width;
    _height = dirtyRect.Height;
    _gridLineFullThickness = _width / 100;
    _gridLineHalfThickness = _gridLineFullThickness / 2;
    _squareWidth = (_width - _gridLineFullThickness) / 8;
    _squareHeight = (_height - _gridLineFullThickness) / 8;

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

    FillAlternateGridSquares(canvas);
  }

  private void DrawHorizontalGridLines(ICanvas canvas)
  {
    foreach (var row in Enumerable.Range(0, 9))
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
    foreach (var col in Enumerable.Range(0, 9))
    {
      var x = CalculateX(col);
      var y1 = 0;
      var y2 = _height;
      canvas.StrokeColor = _gridColour;
      canvas.StrokeSize = _gridLineFullThickness;
      canvas.DrawLine(x, y1, x, y2);
    }
  }

  private void FillAlternateGridSquares(ICanvas canvas)
  {
    foreach (var row in Enumerable.Range(0, 8))
    {
      foreach (var col in Enumerable.Range(0, 8))
      {
        if ((row + col) % 2 == 0)
        {
          var x = CalculateX(col);
          var y = CalculateY(row);
          var width = _squareWidth;
          var height = _squareHeight;
          var rect = new RectF(x, y, width, height);
          var factor = -0.1f;
          rect = rect.Inflate(_squareWidth * factor, _squareHeight * factor);
          canvas.FillColor = _gridColour;
          canvas.FillRectangle(rect.X, rect.Y, rect.Width, rect.Height);
        }
      }
    }
  }

  private void DrawPieces(ICanvas canvas)
  {
    var solutionInternalRows = _whatToDraw.SolutionInternalRows.Cast<DraughtboardPuzzleInternalRow>();
    foreach (var internalRow in solutionInternalRows)
    {
      DrawPiece(canvas, internalRow);
    }
  }

  private void DrawPiece(ICanvas canvas, DraughtboardPuzzleInternalRow internalRow)
  {
    foreach (var square in internalRow.Variation.Squares)
    {
      var coords = square.Coords;
      var row = internalRow.Location.Row + coords.Row;
      var col = internalRow.Location.Col + coords.Col;
      var squareColour = square.Colour == Colour.Black ? Colors.Black : Colors.White;
      var labelColour = square.Colour == Colour.Black ? Colors.White : Colors.Black;
      DrawSquare(canvas, row, col, squareColour);
      DrawLabel(canvas, row, col, internalRow.Label, labelColour);
    }

    DrawPieceBorder(canvas, internalRow);
  }

  private void DrawSquare(ICanvas canvas, int row, int col, Color colour)
  {
    var x = CalculateX(col);
    var y = CalculateY(row);
    var width = _squareWidth;
    var height = _squareHeight;

    canvas.FillColor = colour;
    canvas.FillRectangle(x, y, width, height);
  }

  private void DrawLabel(ICanvas canvas, int row, int col, string label, Color colour)
  {
    var showLabels = (bool)_whatToDraw.DemoDrawingOptions;
    if (!showLabels) return;

    var x = CalculateX(col);
    var y = CalculateY(row);
    var width = _squareWidth;
    var height = _squareHeight;

    canvas.FontColor = colour;
    canvas.FontSize = _squareWidth * 0.25f;
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

  private void DrawPieceBorder(ICanvas canvas, DraughtboardPuzzleInternalRow internalRow)
  {
    var outsideEdges = DrawableUtils.GatherOutsideEdges(internalRow);
    var borderLocations = DrawableUtils.OutsideEdgesToBorderLocations(outsideEdges);
    var path = CreateBorderPath(borderLocations);

    canvas.SaveState();
    canvas.StrokeColor = _borderColour;
    canvas.StrokeSize = _squareWidth * 0.1f;
    canvas.StrokeLineJoin = LineJoin.Round;
    canvas.DrawPath(path);
    canvas.RestoreState();
  }

  private PathF CreateBorderPath(List<Coords> borderLocations)
  {
    var path = new PathF();
    path.MoveTo(CalculatePoint(borderLocations.First()));
    foreach (var location in borderLocations.Skip(1))
    {
      path.LineTo(CalculatePoint(location));
    }
    path.Close();
    return path;
  }

  private float CalculateX(int col) => col * _squareWidth + _gridLineHalfThickness;
  private float CalculateY(int row) => row * _squareHeight + _gridLineHalfThickness;

  private PointF CalculatePoint(Coords coords) =>
    new PointF(CalculateX(coords.Col), CalculateY(coords.Row));
}
