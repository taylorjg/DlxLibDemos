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

  public NonogramDrawable(IWhatToDraw whatToDraw)
  {
    _whatToDraw = whatToDraw;
  }

  public void Draw(ICanvas canvas, RectF dirtyRect)
  {
    var puzzle = (Puzzle)_whatToDraw.DemoSettings;

    _width = dirtyRect.Width;
    _height = dirtyRect.Height;
    _gridLineFullThickness = _width / 400;
    _gridLineHalfThickness = _gridLineFullThickness / 2;
    _squareWidth = (_width - _gridLineFullThickness) / puzzle.Size;
    _squareHeight = (_height - _gridLineFullThickness) / puzzle.Size;

    DrawBackground(canvas);
    DrawGrid(canvas);
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
    var puzzle = (Puzzle)_whatToDraw.DemoSettings;

    foreach (var row in Enumerable.Range(0, puzzle.Size + 1))
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
    var puzzle = (Puzzle)_whatToDraw.DemoSettings;

    foreach (var col in Enumerable.Range(0, puzzle.Size + 1))
    {
      var x = CalculateX(col);
      var y1 = _gridLineHalfThickness;
      var y2 = _height - _gridLineHalfThickness;
      canvas.StrokeColor = Colors.Black;
      canvas.StrokeSize = _gridLineFullThickness;
      canvas.DrawLine(x, y1, x, y2);
    }
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

  private float CalculateX(int col) => col * _squareWidth + _gridLineHalfThickness;
  private float CalculateY(int row) => row * _squareHeight + _gridLineHalfThickness;

  private PointF CalculatePoint(Coords coords) =>
    new PointF(CalculateX(coords.Col), CalculateY(coords.Row));
}
