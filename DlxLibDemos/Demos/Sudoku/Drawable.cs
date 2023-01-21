namespace DlxLibDemos.Demos.Sudoku;

public class SudokuDrawable : IDrawable
{
  private IWhatToDraw _whatToDraw;
  private float _width;
  private float _height;
  private float _gridLineFullThickness;
  private float _gridLineHalfThickness;
  private float _squareWidth;
  private float _squareHeight;

  public SudokuDrawable(IWhatToDraw whatToDraw)
  {
    _whatToDraw = whatToDraw;
  }

  public void Draw(ICanvas canvas, RectF dirtyRect)
  {
    _width = dirtyRect.Width;
    _height = dirtyRect.Height;
    _gridLineFullThickness = _width / 100;
    _gridLineHalfThickness = _gridLineFullThickness / 2;
    _squareWidth = (_width - _gridLineFullThickness) / 9;
    _squareHeight = (_height - _gridLineFullThickness) / 9;

    DrawBackground(canvas);
    DrawGrid(canvas);
    DrawInitialValues(canvas);
    DrawSolvedValues(canvas);
  }

  private void DrawBackground(ICanvas canvas)
  {
    canvas.FillColor = Colors.White;
    canvas.FillRectangle(0, 0, _width, _height);
  }

  private void DrawGrid(ICanvas canvas)
  {
    DrawHorizontalGridLines(canvas);
    DrawVerticalGridLines(canvas);
  }

  private void DrawHorizontalGridLines(ICanvas canvas)
  {
    foreach (var row in Enumerable.Range(0, 10))
    {
      var isThickLine = row % 3 == 0;
      var lineThickness = isThickLine ? _gridLineFullThickness : _gridLineHalfThickness;
      var x1 = 0;
      var x2 = _width;
      var y = CalculateY(row);
      canvas.StrokeColor = Colors.Black;
      canvas.StrokeSize = lineThickness;
      canvas.DrawLine(x1, y, x2, y);
    }
  }

  private void DrawVerticalGridLines(ICanvas canvas)
  {
    foreach (var col in Enumerable.Range(0, 10))
    {
      var isThickLine = col % 3 == 0;
      var lineThickness = isThickLine ? _gridLineFullThickness : _gridLineHalfThickness;
      var x = CalculateX(col);
      var y1 = 0;
      var y2 = _height;
      canvas.StrokeColor = Colors.Black;
      canvas.StrokeSize = lineThickness;
      canvas.DrawLine(x, y1, x, y2);
    }
  }

  private void DrawInitialValues(ICanvas canvas)
  {
    var selectedPuzzle = (Puzzle)_whatToDraw.DemoSettings;
    var initialValues = selectedPuzzle.InitialValues;

    foreach (var initialValue in initialValues)
    {
      DrawDigit(canvas, initialValue.Coords, initialValue.Value, true);
    }
  }

  private void DrawSolvedValues(ICanvas canvas)
  {
    var internalRows = _whatToDraw.SolutionInternalRows
      .Cast<SudokuInternalRow>()
      .Where(internalRow => !internalRow.IsInitialValue);

    foreach (var internalRow in internalRows)
    {
      DrawDigit(canvas, internalRow.Coords, internalRow.Value, false);
    }
  }

  private void DrawDigit(ICanvas canvas, Coords coords, int value, bool isInitialValue)
  {
    var (row, col) = coords;
    var valueString = value.ToString();
    var x = CalculateX(col);
    var y = CalculateY(row);
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
}
