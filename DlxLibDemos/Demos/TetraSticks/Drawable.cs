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

  private static readonly Dictionary<string, Color> PieceColours = new Dictionary<string, Color>
  {
    { "F", Color.FromRgba("#FF7366") },
    { "H", Color.FromRgba("#00E61A") },
    { "I", Color.FromRgba("#660066") },
    { "J", Color.FromRgba("#E6E6FF") },
    { "L", Color.FromRgba("#596673") },
    { "N", Color.FromRgba("#FFFF00") },
    { "O", Color.FromRgba("#CCCC1A") },
    { "P", Color.FromRgba("#994D33") },
    { "R", Color.FromRgba("#9926B2") },
    { "T", Color.FromRgba("#3300B2") },
    { "U", Color.FromRgba("#FF2699") },
    { "V", Color.FromRgba("#00FFFF") },
    { "W", Color.FromRgba("#CCFF00") },
    { "X", Color.FromRgba("#E60000") },
    { "Y", Color.FromRgba("#6659E6") },
    { "Z", Color.FromRgba("#008000") }
  };

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
    DrawPieces(canvas);

    // var pieceWithVariations = PiecesWithVariations.ThePiecesWithVariations[8];
    // var label = pieceWithVariations.Label;
    // var variation1 = pieceWithVariations.Variations[0];
    // var variation2 = pieceWithVariations.Variations[1];

    // DrawPiece(canvas, new TetraSticksInternalRow(label, variation1, new Coords(0, 0)));
    // DrawPiece(canvas, new TetraSticksInternalRow(label, variation2, new Coords(0, 4)));
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

  private void DrawPieces(ICanvas canvas)
  {
    var solutionInternalRows = _whatToDraw.SolutionInternalRows.Cast<TetraSticksInternalRow>();

    foreach (var internalRow in solutionInternalRows)
    {
      DrawPiece(canvas, internalRow);
    }
  }

  private void DrawPiece(ICanvas canvas, TetraSticksInternalRow internalRow)
  {
    var colour = PieceColours[internalRow.Label];

    var location = internalRow.Location;
    var variation = internalRow.Variation;

    foreach (var horizontal in variation.Horizontals)
    {
      var w = _squareWidth;
      var h = _squareHeight * 0.1f;
      var x = CalculateX(location.Col + horizontal.Col);
      var y = CalculateY(location.Row + horizontal.Row) - h / 2;
      canvas.FillColor = colour;
      canvas.FillRectangle(x, y, w, h);
    }

    foreach (var vertical in variation.Verticals)
    {
      var w = _squareWidth * 0.1f;
      var h = _squareHeight;
      var x = CalculateX(location.Col + vertical.Col) - w / 2;
      var y = CalculateY(location.Row + vertical.Row);
      canvas.FillColor = colour;
      canvas.FillRectangle(x, y, w, h);
    }

    foreach (var junction in variation.Junctions)
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
