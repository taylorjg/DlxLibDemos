namespace DlxLibDemos.Demos.NQueens;

public class NQueensDrawable : IDrawable
{
  private IWhatToDraw _whatToDraw;
  private float _squareWidth;
  private float _squareHeight;

  private int N;

  public NQueensDrawable(IWhatToDraw whatToDraw)
  {
    _whatToDraw = whatToDraw;
  }

  public void Draw(ICanvas canvas, RectF dirtyRect)
  {
    N = (int)_whatToDraw.DemoSettings;
    _squareWidth = dirtyRect.Width / N;
    _squareHeight = dirtyRect.Height / N;
    canvas.FillColor = Colors.White;
    canvas.FillRectangle(0, 0, dirtyRect.Width, dirtyRect.Height);
    DrawGrid(canvas);
    var solutionInternalRows = _whatToDraw.SolutionInternalRows.Cast<NQueensInternalRow>();
    foreach (var internalRow in solutionInternalRows)
    {
      var row = internalRow.Coords.Row;
      var col = internalRow.Coords.Col;
      DrawQueen(canvas, row, col);
    }
  }

  private void DrawGrid(ICanvas canvas)
  {
    var locations =
      Enumerable.Range(0, N).SelectMany(row =>
        Enumerable.Range(0, N).Select(col =>
          new Coords(row, col))).ToArray();

    foreach (var coords in locations)
    {
      var row = coords.Row;
      var col = coords.Col;
      var colour = (row + col) % 2 == 0 ? Colors.Peru : Colors.SandyBrown;
      DrawSquare(canvas, row, col, colour);
    }
  }

  private void DrawSquare(ICanvas canvas, int row, int col, Color colour)
  {
    var x = _squareWidth * col;
    var y = _squareHeight * row;
    var width = _squareWidth;
    var height = _squareHeight;

    canvas.FillColor = colour;
    canvas.FillRectangle(x, y, width, height);
  }

  private void DrawQueen(ICanvas canvas, int row, int col)
  {
    var x = _squareWidth * col;
    var y = _squareHeight * row;
    var width = _squareWidth;
    var height = _squareHeight;

    // Unicode white chess queen
    // https://util.unicode.org/UnicodeJsps/character.jsp?a=2655
    var text = "\u2655";

    canvas.FontColor = Colors.White;
    canvas.FontSize = _squareWidth * 0.8f;
    canvas.DrawString(text, x, y, width, height, HorizontalAlignment.Center, VerticalAlignment.Center);
  }
}
