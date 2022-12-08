namespace DlxLibDemos.Demos.TetraSticks;

public record Coords(int Row, int Col)
{
  public Coords Add(Coords other) => new Coords(Row + other.Row, Col + other.Col);
};
