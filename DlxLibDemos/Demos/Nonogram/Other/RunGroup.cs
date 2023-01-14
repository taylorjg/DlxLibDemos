namespace DlxLibDemos.Demos.Nonogram;

public abstract record RunGroup(RunGroupType RunGroupType, int[] Lengths)
{
  public abstract Coords MakeCoords(int otherValue);
}
