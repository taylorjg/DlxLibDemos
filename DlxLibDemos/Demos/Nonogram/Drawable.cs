namespace DlxLibDemos.Demos.Nonogram;

public class NonogramDrawable : IDrawable
{
  private IWhatToDraw _whatToDraw;
  private float _width;
  private float _height;
  private float _squareWidth;
  private float _squareHeight;
  private float _gridLineFullThickness;
  private float _gridLineHalfThickness;
  private Puzzle _puzzle;
  private int _numMarginSquares;
  private int _sizeWithMargin;

  public NonogramDrawable(IWhatToDraw whatToDraw)
  {
    _whatToDraw = whatToDraw;
  }

  public void Draw(ICanvas canvas, RectF dirtyRect)
  {
    _puzzle = (Puzzle)_whatToDraw.DemoSettings;

    var showClues = (bool)_whatToDraw.DemoOptionalSettings;
    if (showClues)
    {
      var maxNumRunGroupsHorizontally = _puzzle.HorizontalRunGroups.Select(rg => rg.Lengths.Length).Max();
      var maxNumRunGroupsVertically = _puzzle.VerticalRunGroups.Select(rg => rg.Lengths.Length).Max();
      _numMarginSquares = Math.Max(maxNumRunGroupsHorizontally, maxNumRunGroupsVertically);
    }
    else
    {
      _numMarginSquares = 0;
    }

    _sizeWithMargin = _numMarginSquares + _puzzle.Size;

    _width = dirtyRect.Width;
    _height = dirtyRect.Height;
    _gridLineFullThickness = _width / 400;
    _gridLineHalfThickness = _gridLineFullThickness / 2;
    _squareWidth = (_width - _gridLineFullThickness) / _sizeWithMargin;
    _squareHeight = (_height - _gridLineFullThickness) / _sizeWithMargin;

    DrawBackground(canvas);
    DrawGrid(canvas);
    DrawHorizontalRunLengths(canvas);
    DrawVerticalRunLengths(canvas);
    DrawHorizontalRunGroups(canvas);
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

  private void DrawHorizontalRunLengths(ICanvas canvas)
  {
    var showClues = (bool)_whatToDraw.DemoOptionalSettings;
    if (!showClues) return;

    foreach (var runGroup in _puzzle.HorizontalRunGroups)
    {
      var horizontalRunGroup = runGroup as HorizontalRunGroup;
      var row = horizontalRunGroup.Row;
      var numRunLengths = runGroup.Lengths.Length;
      foreach (var index in Enumerable.Range(0, numRunLengths))
      {
        var runLength = runGroup.Lengths[index];
        var col = -(numRunLengths - index);
        var coords = new Coords(row, col);
        DrawRunLength(canvas, coords, runLength);
      }
    }
  }

  private void DrawVerticalRunLengths(ICanvas canvas)
  {
    var showClues = (bool)_whatToDraw.DemoOptionalSettings;
    if (!showClues) return;

    foreach (var runGroup in _puzzle.VerticalRunGroups)
    {
      var verticalRunGroup = runGroup as VerticalRunGroup;
      var col = verticalRunGroup.Col;
      var numRunLengths = runGroup.Lengths.Length;
      foreach (var index in Enumerable.Range(0, numRunLengths))
      {
        var runLength = runGroup.Lengths[index];
        var row = -(numRunLengths - index);
        var coords = new Coords(row, col);
        DrawRunLength(canvas, coords, runLength);
      }
    }
  }

  private void DrawRunLength(ICanvas canvas, Coords coords, int runLength)
  {
    var runLengthString = runLength.ToString();
    var x = CalculateX(coords.Col);
    var y = CalculateY(coords.Row);
    var width = _squareWidth;
    var height = _squareHeight;
    canvas.FontColor = Colors.Black;
    canvas.FontSize = _squareWidth * 0.6f;
    canvas.DrawString(
      runLengthString,
      x,
      y,
      width,
      height,
      HorizontalAlignment.Center,
      VerticalAlignment.Center
    );
  }

  private void DrawHorizontalRunGroups(ICanvas canvas)
  {
    var solutionInternalRows = _whatToDraw.SolutionInternalRows.Cast<NonogramInternalRow>();

    foreach (var internalRow in solutionInternalRows)
    {
      if (internalRow.RunGroup.RunGroupType == RunGroupType.Horizontal)
      {
        DrawHorizontalRunGroup(canvas, internalRow);
      }
    }
  }

  private void DrawHorizontalRunGroup(ICanvas canvas, NonogramInternalRow internalRow)
  {
    foreach (var RunCoordsList in internalRow.RunCoordsLists)
    {
      DrawHorizontalRun(canvas, RunCoordsList);
    }
  }

  private void DrawHorizontalRun(ICanvas canvas, RunCoordsList runCoordsList)
  {
    foreach (var coords in runCoordsList.CoordsList)
    {
      DrawBlock(canvas, coords);
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

  private float CalculateX(int col) => (_numMarginSquares + col) * _squareWidth + _gridLineHalfThickness;
  private float CalculateY(int row) => (_numMarginSquares + row) * _squareHeight + _gridLineHalfThickness;

  private PointF CalculatePoint(Coords coords) =>
    new PointF(CalculateX(coords.Col), CalculateY(coords.Row));
}
