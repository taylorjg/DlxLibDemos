namespace DlxLibDemos.Demos.RippleEffect;

public class RippleEffectDrawable : IDrawable
{
  private IWhatToDraw _whatToDraw;
  private float _width;
  private float _height;
  private float _gridLineFullThickness;
  private float _gridLineHalfThickness;
  private float _borderLineFullThickness;
  private float _borderLineHalfThickness;
  private float _squareWidth;
  private float _squareHeight;
  private readonly Color _gridColour = Colors.Black;
  private readonly Color _borderColour = Colors.Black;

  public RippleEffectDrawable(IWhatToDraw whatToDraw)
  {
    _whatToDraw = whatToDraw;
  }

  public void Draw(ICanvas canvas, RectF dirtyRect)
  {
    _width = dirtyRect.Width;
    _height = dirtyRect.Height;
    _gridLineFullThickness = _width / 400;
    _gridLineHalfThickness = _gridLineFullThickness / 2;
    _borderLineFullThickness = _gridLineFullThickness * 4;
    _borderLineHalfThickness = _borderLineFullThickness / 2;
    _squareWidth = (_width - _borderLineFullThickness) / 8;
    _squareHeight = (_height - _borderLineFullThickness) / 8;

    DrawBackground(canvas);
    DrawGrid(canvas);
    DrawRooms(canvas);
    DrawInitialValues(canvas);
    DrawCalculatedValues(canvas);
  }

  private void DrawBackground(ICanvas canvas)
  {
    var x = _borderLineHalfThickness;
    var y = _borderLineHalfThickness;
    var w = _width - _borderLineFullThickness;
    var h = _height - _borderLineFullThickness;

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
    foreach (var row in Enumerable.Range(0, 9))
    {
      var x1 = _borderLineHalfThickness;
      var x2 = _width - _borderLineHalfThickness;
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
      var y1 = _borderLineHalfThickness;
      var y2 = _height - _borderLineHalfThickness;
      canvas.StrokeColor = _gridColour;
      canvas.StrokeSize = _gridLineFullThickness;
      canvas.DrawLine(x, y1, x, y2);
    }
  }

  private void DrawRooms(ICanvas canvas)
  {
    var puzzle = (Puzzle)_whatToDraw.DemoSettings;

    foreach (var room in puzzle.Rooms)
    {
      DrawRoom(canvas, room);
    }
  }

  private void DrawRoom(ICanvas canvas, Room room)
  {
    var outsideEdges = DrawableUtils.GatherOutsideEdges(room);
    var borderLocations = DrawableUtils.OutsideEdgesToBorderLocations(outsideEdges);
    var path = CreateBorderPath(borderLocations);

    canvas.SaveState();
    canvas.StrokeColor = _borderColour;
    canvas.StrokeSize = _borderLineFullThickness;
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

  private void DrawInitialValues(ICanvas canvas)
  {
    var puzzle = (Puzzle)_whatToDraw.DemoSettings;

    foreach (var initialValue in puzzle.InitialValues)
    {
      DrawDigit(
        canvas,
        initialValue.Cell,
        initialValue.Value,
        true);
    }
  }

  private void DrawCalculatedValues(ICanvas canvas)
  {
    var internalRows = _whatToDraw.SolutionInternalRows
      .Cast<RippleEffectInternalRow>()
      .Where(internalRow => !internalRow.IsInitialValue);

    foreach (var internalRow in internalRows)
    {
      DrawDigit(
        canvas,
        internalRow.Cell,
        internalRow.Value,
        false);
    }
  }

  private void DrawDigit(ICanvas canvas, Coords cell, int value, bool isInitialValue)
  {
    var valueString = value.ToString();
    var x = CalculateX(cell.Col);
    var y = CalculateY(cell.Row);
    var width = _squareWidth;
    var height = _squareHeight;
    canvas.FontColor = isInitialValue ? Colors.Magenta : Colors.Black;
    canvas.FontSize = _squareWidth * 0.6f;
    canvas.DrawString(
      valueString,
      x,
      y,
      width,
      height,
      HorizontalAlignment.Center,
      VerticalAlignment.Center
    );
  }

  private float CalculateX(int col) => col * _squareWidth + _borderLineHalfThickness;
  private float CalculateY(int row) => row * _squareHeight + _borderLineHalfThickness;

  private PointF CalculatePoint(Coords coords) =>
    new PointF(CalculateX(coords.Col), CalculateY(coords.Row));
}
