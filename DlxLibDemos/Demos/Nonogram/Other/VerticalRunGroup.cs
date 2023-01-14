namespace DlxLibDemos.Demos.Nonogram;

public record VerticalRunGroup(int[] Lengths, int Col) : RunGroup(RunGroupType.Vertical, Lengths)
{
  public override Coords MakeCoords(int row) => new Coords(row, Col);
}
