namespace DlxLibDemos.Demos.TetraSticks;

public class TetraSticksDrawable : IDrawable
{
  private IWhatToDraw _whatToDraw;
  private float _width;
  private float _height;
  private float _gridLineFullThickness;
  private float _gridLineHalfThickness;
  private float _squareWidth;
  private float _squareHeight;
  private readonly Color _gridColour = Color.FromRgba("#CD853F80");

  public TetraSticksDrawable(IWhatToDraw whatToDraw)
  {
    _whatToDraw = whatToDraw;
  }

  public void Draw(ICanvas canvas, RectF dirtyRect)
  {
    _width = dirtyRect.Width;
    _height = dirtyRect.Height;
    _gridLineFullThickness = dirtyRect.Width / 100;
    _gridLineHalfThickness = _gridLineFullThickness / 2;
    _squareWidth = (dirtyRect.Width - _gridLineFullThickness) / 8;
    _squareHeight = (dirtyRect.Height - _gridLineFullThickness) / 8;

    DrawGrid(canvas);

    DrawPiece(canvas, new Coords(0, 0), Pieces.ThePieces[9]);
    DrawPiece(canvas, new Coords(0, 4), Pieces.ThePieces[9].Reflect());

    // DrawPiece(canvas, new Coords(0, 0), Pieces.ThePieces[0]);
    // DrawPiece(canvas, new Coords(0, 4), Pieces.ThePieces[1]);
    // DrawPiece(canvas, new Coords(4, 0), Pieces.ThePieces[2]);
    // DrawPiece(canvas, new Coords(4, 4), Pieces.ThePieces[3]);

    // DrawPiece(canvas, new Coords(0, 0), Pieces.ThePieces[4]);
    // DrawPiece(canvas, new Coords(0, 4), Pieces.ThePieces[5]);
    // DrawPiece(canvas, new Coords(4, 0), Pieces.ThePieces[6]);
    // DrawPiece(canvas, new Coords(4, 4), Pieces.ThePieces[7]);

    // DrawPiece(canvas, new Coords(0, 0), Pieces.ThePieces[8]);
    // DrawPiece(canvas, new Coords(0, 4), Pieces.ThePieces[9]);
    // DrawPiece(canvas, new Coords(4, 0), Pieces.ThePieces[10]);
    // DrawPiece(canvas, new Coords(4, 4), Pieces.ThePieces[11]);

    // DrawPiece(canvas, new Coords(0, 0), Pieces.ThePieces[12]);
    // DrawPiece(canvas, new Coords(0, 4), Pieces.ThePieces[13]);
    // DrawPiece(canvas, new Coords(4, 0), Pieces.ThePieces[14]);
    // DrawPiece(canvas, new Coords(4, 4), Pieces.ThePieces[15]);
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

  private void DrawPiece(ICanvas canvas, Coords location, Piece piece)
  {
    foreach (var horizontal in piece.Horizontals)
    {
      var w = _squareWidth;
      var h = _squareHeight * 0.1f;
      var x = CalculateX(location.Col + horizontal.Col);
      var y = CalculateY(location.Row + horizontal.Row) - h / 2;
      canvas.FillColor = Colors.Red;
      canvas.FillRectangle(x, y, w, h);
    }

    foreach (var vertical in piece.Verticals)
    {
      var w = _squareWidth * 0.1f;
      var h = _squareHeight;
      var x = CalculateX(location.Col + vertical.Col) - w / 2;
      var y = CalculateY(location.Row + vertical.Row);
      canvas.FillColor = Colors.Red;
      canvas.FillRectangle(x, y, w, h);
    }

    foreach (var junction in piece.Junctions)
    {
      var cx = CalculateX(location.Col + junction.Col);
      var cy = CalculateY(location.Row + junction.Row);
      var r = _squareWidth * 0.05f;
      canvas.FillColor = Colors.Purple;
      canvas.FillCircle(cx, cy, r);
    }
  }

  private float CalculateX(int col) => col * _squareWidth + _gridLineHalfThickness;
  private float CalculateY(int row) => row * _squareHeight + _gridLineHalfThickness;

  private PointF CalculatePoint(Coords coords) =>
    new PointF(CalculateX(coords.Col), CalculateY(coords.Row));
}
