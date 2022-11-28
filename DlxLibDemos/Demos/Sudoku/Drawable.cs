namespace DlxLibDemos.Demos.Sudoku;

public class SudokuDrawable : IDrawable
{
  private IWhatToDraw _whatToDraw;
  private float _gridLineFullThickness;
  private float _gridLineHalfThickness;
  private float _gridLineQuarterThickness;
  private float _squareWidth;
  private float _squareHeight;

  public SudokuDrawable(IWhatToDraw whatToDraw)
  {
    _whatToDraw = whatToDraw;
  }

  public void Draw(ICanvas canvas, RectF dirtyRect)
  {
    _gridLineFullThickness = dirtyRect.Width / 100;
    _gridLineHalfThickness = _gridLineFullThickness / 2;
    _gridLineQuarterThickness = _gridLineFullThickness / 4;
    _squareWidth = (dirtyRect.Width - _gridLineFullThickness) / 9;
    _squareHeight = (dirtyRect.Height - _gridLineFullThickness) / 9;
    canvas.FillColor = Colors.White;
    canvas.FillRectangle(0, 0, dirtyRect.Width, dirtyRect.Height);
    DrawGrid(canvas);
    DrawInitialValues(canvas);
    DrawCalculatedValues(canvas);
  }

  private void DrawGrid(ICanvas canvas)
  {
    DrawHorizontalGridLines(canvas);
    DrawVerticalGridLines(canvas);
  }

  private void DrawInitialValues(ICanvas canvas)
  {
    var selectedPuzzle = (Puzzle)_whatToDraw.DemoSettings;
    var internalRows = selectedPuzzle.InternalRows;

    foreach (var internalRow in internalRows)
    {
      DrawDigit(
        canvas,
        internalRow.Coords.Row,
        internalRow.Coords.Col,
        internalRow.Value,
        true);
    }
  }

  private void DrawCalculatedValues(ICanvas canvas)
  {
    var internalRows = _whatToDraw.SolutionInternalRows
      .Cast<SudokuInternalRow>()
      .Where(internalRow => !internalRow.IsInitialValue);

    foreach (var internalRow in internalRows)
    {
      DrawDigit(
        canvas,
        internalRow.Coords.Row,
        internalRow.Coords.Col,
        internalRow.Value,
        false);
    }
  }

  private void DrawDigit(ICanvas canvas, int row, int col, int value, bool isInitialValue)
  {
    var valueString = value.ToString();
    var x = _squareWidth * col + _gridLineHalfThickness;
    var y = _squareHeight * row + _gridLineHalfThickness;
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

  private void DrawHorizontalGridLines(ICanvas canvas)
  {
    foreach (var row in Enumerable.Range(0, 10))
    {
      var isThickLine = row % 3 == 0;
      var full = isThickLine ? _gridLineFullThickness : _gridLineHalfThickness;
      var half = isThickLine ? _gridLineHalfThickness : _gridLineQuarterThickness;
      var x1 = 0;
      var y1 = _squareHeight * row + half;
      var x2 = 9 * _squareWidth + _gridLineFullThickness;
      var y2 = _squareHeight * row + half;
      canvas.StrokeColor = Colors.Black;
      canvas.StrokeSize = full;
      canvas.DrawLine(x1, y1, x2, y2);
    }
  }

  private void DrawVerticalGridLines(ICanvas canvas)
  {
    foreach (var col in Enumerable.Range(0, 10))
    {
      var isThickLine = col % 3 == 0;
      var full = isThickLine ? _gridLineFullThickness : _gridLineHalfThickness;
      var half = isThickLine ? _gridLineHalfThickness : _gridLineQuarterThickness;
      var x1 = _squareWidth * col + half;
      var y1 = _gridLineHalfThickness;
      var x2 = _squareWidth * col + half;
      var y2 = _gridLineHalfThickness + 9 * _squareHeight;
      canvas.StrokeColor = Colors.Black;
      canvas.StrokeSize = full;
      canvas.DrawLine(x1, y1, x2, y2);
    }
  }
}
