namespace DlxLibDemos.Demos.RippleEffect;

public class RippleEffectDrawable : IDrawable
{
  private IWhatToDraw _whatToDraw;
  private float _width;
  private float _height;
  private float _gridLineFullThickness;
  private float _gridLineHalfThickness;
  private float _squareWidth;
  private float _squareHeight;
  private readonly Color _gridColour = Colors.Black;
  private readonly Color _borderColour = Color.FromRgba("#0066CC");

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
    _squareWidth = (_width - _gridLineFullThickness) / 8;
    _squareHeight = (_height - _gridLineFullThickness) / 8;

    DrawBackground(canvas);
    DrawGrid(canvas);
    DrawRooms(canvas);
    DrawInitialValues(canvas);
    DrawCalculatedValues(canvas);
  }

  private void DrawBackground(ICanvas canvas)
  {
    canvas.FillColor = Colors.White;
    canvas.FillRectangle(0, 0, _width, _height);
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
    foreach (var col in Enumerable.Range(0, 9))
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

  private void DrawRooms(ICanvas canvas)
  {
    // var puzzle = (Puzzle)_whatToDraw.DemoSettings;
    var puzzle = Puzzles.ThePuzzles[0];

    foreach (var room in puzzle.Rooms)
    {
      DrawRoom(canvas, room);
    }
  }

  private void DrawRoom(ICanvas canvas, Room room)
  {
  }

  private void DrawInitialValues(ICanvas canvas)
  {
    // var puzzle = (Puzzle)_whatToDraw.DemoSettings;
    var puzzle = Puzzles.ThePuzzles[0];

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
    var x = _squareWidth * cell.Col + _gridLineHalfThickness;
    var y = _squareHeight * cell.Row + _gridLineHalfThickness;
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

  private float CalculateX(int col) => col * _squareWidth + _gridLineHalfThickness;
  private float CalculateY(int row) => row * _squareHeight + _gridLineHalfThickness;

  private PointF CalculatePoint(Coords coords) =>
    new PointF(CalculateX(coords.Col), CalculateY(coords.Row));
}
