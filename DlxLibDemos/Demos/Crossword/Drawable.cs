namespace DlxLibDemos.Demos.Crossword;

public class CrosswordDrawable : IDrawable
{
  private IWhatToDraw _whatToDraw;
  private float _width;
  private float _height;
  private float _squareWidth;
  private float _squareHeight;
  private float _gridLineFullThickness;
  private float _gridLineHalfThickness;
  private Puzzle _puzzle;

  public CrosswordDrawable(IWhatToDraw whatToDraw)
  {
    _whatToDraw = whatToDraw;
  }

  public void Draw(ICanvas canvas, RectF dirtyRect)
  {
    _puzzle = Puzzles.ThePuzzles.First();

    _width = dirtyRect.Width;
    _height = dirtyRect.Height;
    _gridLineFullThickness = _width / 400;
    _gridLineHalfThickness = _gridLineFullThickness / 2;
    _squareWidth = (_width - _gridLineFullThickness) / _puzzle.Size;
    _squareHeight = (_height - _gridLineFullThickness) / _puzzle.Size;

    DrawBackground(canvas);
    DrawGrid(canvas);
    DrawBlocks(canvas);
    DrawClueNumbers(canvas);
    DrawAnswers(canvas);
  }

  private void DrawBackground(ICanvas canvas)
  {
    var x = _gridLineHalfThickness;
    var y = _gridLineHalfThickness;
    var w = _width - _gridLineFullThickness;
    var h = _height - _gridLineFullThickness;

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
    foreach (var row in Enumerable.Range(0, _puzzle.Size + 1))
    {
      var x1 = _gridLineHalfThickness;
      var x2 = _width - _gridLineHalfThickness;
      var y = CalculateY(row);
      canvas.StrokeColor = Colors.Black;
      canvas.StrokeSize = _gridLineFullThickness;
      canvas.DrawLine(x1, y, x2, y);
    }
  }

  private void DrawVerticalGridLines(ICanvas canvas)
  {
    foreach (var col in Enumerable.Range(0, _puzzle.Size + 1))
    {
      var x = CalculateX(col);
      var y1 = _gridLineHalfThickness;
      var y2 = _height - _gridLineHalfThickness;
      canvas.StrokeColor = Colors.Black;
      canvas.StrokeSize = _gridLineFullThickness;
      canvas.DrawLine(x, y1, x, y2);
    }
  }

  private void DrawBlocks(ICanvas canvas)
  {
    foreach (var block in _puzzle.Blocks)
    {
      DrawBlock(canvas, block);
    }
  }

  private void DrawBlock(ICanvas canvas, Coords coords)
  {
    var x = CalculateX(coords.Col);
    var y = CalculateY(coords.Row);
    var w = _squareWidth;
    var h = _squareHeight;

    canvas.FillColor = Colors.Black;
    canvas.FillRectangle(x, y, w, h);
  }

  private void DrawClueNumbers(ICanvas canvas)
  {
    foreach (var clue in _puzzle.Clues)
    {
      DrawClueNumber(canvas, clue.CoordsList.First(), clue.ClueNumber);
    }
  }

  private void DrawClueNumber(ICanvas canvas, Coords coords, int clueNumber)
  {
    var (row, col) = coords;
    var clueNumberString = clueNumber.ToString();
    var x = CalculateX(col);
    var y = CalculateY(row);
    var factor = 4;
    var width = _squareWidth / factor;
    var height = _squareHeight / factor;
    canvas.FontColor = Colors.Black;
    canvas.FontSize = _squareWidth * 0.2f;
    canvas.DrawString(
      clueNumberString,
      x + width / 4,
      y + height / 4,
      width,
      height,
      HorizontalAlignment.Left,
      VerticalAlignment.Center
    );
  }

  private void DrawAnswers(ICanvas canvas)
  {
    var internalRows = _whatToDraw.SolutionInternalRows.Cast<CrosswordInternalRow>();

    foreach (var internalRow in internalRows)
    {
      foreach (var index in Enumerable.Range(0, internalRow.Answer.Clue.CoordsList.Length))
      {
        var coords = internalRow.Answer.Clue.CoordsList[index];
        var letter = internalRow.Answer.answer[index];
        DrawLetter(canvas, coords, letter);
      }
    }
  }

  private void DrawLetter(ICanvas canvas, Coords coords, char letter)
  {
    var (row, col) = coords;
    var letterString = letter.ToString().ToUpper();
    var x = CalculateX(col);
    var y = CalculateY(row);
    var width = _squareWidth;
    var height = _squareHeight;
    canvas.FontColor = Colors.Black;
    canvas.FontSize = _squareWidth * 0.6f;
    canvas.DrawString(
      letterString,
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
