namespace DlxLibDemos.Demos.FlowFree;

public record Coords(int Row, int Col)
{
  public Coords Add(Coords other) => new Coords(Row + other.Row, Col + other.Col);
  public Coords Left() => new Coords(Row, Col - 1);
  public Coords Right() => new Coords(Row, Col + 1);
  public Coords Up() => new Coords(Row - 1, Col);
  public Coords Down() => new Coords(Row + 1, Col);
};
