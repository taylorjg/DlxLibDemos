namespace DlxLibDemos.Demos.Nonogram;

public record HorizontalRunGroup(int[] Lengths, int Row) : RunGroup(RunGroupType.Horizontal, Lengths);
