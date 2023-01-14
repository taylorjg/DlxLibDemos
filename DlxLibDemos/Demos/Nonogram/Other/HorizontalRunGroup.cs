namespace DlxLibDemos.Demos.Nonogram;

public record HorizontalRunGroup(int[] Lengths, int Row) : RunGroup(RunGroupType.Horizontal, Lengths)
{
  public override Coords MakeCoords(int col) => new Coords(Row, col);
}
