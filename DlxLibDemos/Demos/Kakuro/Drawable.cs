namespace DlxLibDemos.Demos.Kakuro;

public class KakuroDrawable : IDrawable
{
  private IWhatToDraw _whatToDraw;
  private float _width;
  private float _height;
  private float _squareWidth;
  private float _squareHeight;
  private float _gridLineFullThickness;
  private float _gridLineHalfThickness;

  public KakuroDrawable(IWhatToDraw whatToDraw)
  {
    _whatToDraw = whatToDraw;
  }

  public void Draw(ICanvas canvas, RectF dirtyRect)
  {
    _width = dirtyRect.Width;
    _height = dirtyRect.Height;
    _gridLineFullThickness = _width / 400;
    _gridLineHalfThickness = _gridLineFullThickness / 2;
    _squareWidth = (_width - _gridLineFullThickness) / 10;
    _squareHeight = (_height - _gridLineFullThickness) / 10;

    var puzzle = Puzzles.ThePuzzles.First();

    DrawBackground(canvas);
    DrawGrid(canvas);
    DrawBlocks(canvas, puzzle.Blocks);
    DrawClues(canvas, puzzle.Clues);
    DrawHorizontalRuns(canvas);
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
    foreach (var row in Enumerable.Range(0, 10 + 1))
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
    foreach (var col in Enumerable.Range(0, 10 + 1))
    {
      var x = CalculateX(col);
      var y1 = _gridLineHalfThickness;
      var y2 = _height - _gridLineHalfThickness;
      canvas.StrokeColor = Colors.Black;
      canvas.StrokeSize = _gridLineFullThickness;
      canvas.DrawLine(x, y1, x, y2);
    }
  }

  private void DrawBlocks(ICanvas canvas, Coords[] blocks)
  {
    foreach (var block in blocks)
    {
      DrawBlock(canvas, block);
    }
  }

  private void DrawBlock(ICanvas canvas, Coords coords)
  {
    var x = CalculateX(coords.Col);
    var y = CalculateY(coords.Row);
    var width = _squareWidth;
    var height = _squareHeight;

    canvas.FillColor = Colors.Black;
    canvas.FillRectangle(x, y, width, height);
  }

  private void DrawClues(ICanvas canvas, Clue[] clues)
  {
    var showLabels = (bool)_whatToDraw.DemoOptionalSettings;
    if (!showLabels) return;

    foreach (var clue in clues)
    {
      DrawClue(canvas, clue);
    }
  }

  private void DrawClue(ICanvas canvas, Clue clue)
  {
    if (clue.AcrossSum.HasValue)
    {
      DrawAcrossClue(canvas, clue.Coords, clue.AcrossSum.Value);
    }

    if (clue.DownSum.HasValue)
    {
      DrawDownClue(canvas, clue.Coords, clue.DownSum.Value);
    }
  }

  private void DrawAcrossClue(ICanvas canvas, Coords coords, int sum)
  {
    var px = CalculateX(coords.Col);
    var py = CalculateY(coords.Row);
    var p1 = new PointF(px + _gridLineFullThickness, py);
    var p2 = new PointF(px + _squareWidth * 0.85f, py);
    var p3 = new PointF(px + _squareWidth, py + _squareHeight / 4);
    var p4 = new PointF(p2.X, py + _squareHeight / 2);
    var p5 = new PointF(px + _squareWidth / 2 + _gridLineFullThickness, py + _squareHeight / 2);
    var path = new PathF();
    path.MoveTo(p1);
    path.LineTo(p2);
    path.LineTo(p3);
    path.LineTo(p4);
    path.LineTo(p5);
    path.Close();

    canvas.SaveState();
    canvas.FillColor = Colors.White;
    canvas.FillPath(path);
    canvas.RestoreState();

    var sumString = sum.ToString();
    var width = _squareWidth / 2;
    var height = _squareHeight / 2;
    var x = CalculateX(coords.Col) + width - _gridLineFullThickness;
    var y = CalculateY(coords.Row);
    canvas.FontColor = Colors.Black;
    canvas.FontSize = _squareWidth * 0.325f;
    canvas.DrawString(
      sumString,
      x,
      y,
      width,
      height,
      HorizontalAlignment.Center,
      VerticalAlignment.Center
    );
  }

  private void DrawDownClue(ICanvas canvas, Coords coords, int sum)
  {
    var px = CalculateX(coords.Col);
    var py = CalculateY(coords.Row);
    var p1 = new PointF(px, py + _gridLineFullThickness);
    var p2 = new PointF(px, py + _squareHeight * 0.85f);
    var p3 = new PointF(px + _squareWidth / 4, py + _squareHeight);
    var p4 = new PointF(px + _squareWidth / 2, p2.Y);
    var p5 = new PointF(px + _squareWidth / 2, py + _squareHeight / 2 + _gridLineFullThickness);
    var path = new PathF();
    path.MoveTo(p1);
    path.LineTo(p2);
    path.LineTo(p3);
    path.LineTo(p4);
    path.LineTo(p5);
    path.Close();

    canvas.SaveState();
    canvas.FillColor = Colors.White;
    canvas.FillPath(path);
    canvas.RestoreState();

    var sumString = sum.ToString();
    var width = _squareWidth / 2;
    var height = _squareHeight / 2;
    var x = CalculateX(coords.Col);
    var y = CalculateY(coords.Row) + height - _gridLineFullThickness;
    canvas.FontColor = Colors.Black;
    canvas.FontSize = _squareWidth * 0.325f;
    canvas.DrawString(
      sumString,
      x,
      y,
      width,
      height,
      HorizontalAlignment.Center,
      VerticalAlignment.Center
    );
  }

  private void DrawHorizontalRuns(ICanvas canvas)
  {
    var solutionInternalRows = _whatToDraw.SolutionInternalRows.Cast<KakuroInternalRow>();

    foreach (var internalRow in solutionInternalRows)
    {
      if (internalRow.Run.RunType == RunType.Horizontal)
      {
        DrawHorizontalRun(canvas, internalRow);
      }
    }
  }

  private void DrawHorizontalRun(ICanvas canvas, KakuroInternalRow internalRow)
  {
    var run = internalRow.Run;
    var values = internalRow.Values;

    foreach (var index in Enumerable.Range(0, run.CoordsList.Length))
    {
      DrawDigit(canvas, run.CoordsList[index], values[index]);
    }
  }

  private void DrawDigit(ICanvas canvas, Coords coords, int value)
  {
    var valueString = value.ToString();
    var x = CalculateX(coords.Col);
    var y = CalculateY(coords.Row);
    var width = _squareWidth;
    var height = _squareHeight;
    canvas.FontColor = Colors.Black;
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
