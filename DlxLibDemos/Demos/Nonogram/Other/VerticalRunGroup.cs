namespace DlxLibDemos.Demos.Nonogram;

public record VerticalRunGroup(int[] Lengths, int Col) : RunGroup(RunGroupType.Vertical, Lengths);
