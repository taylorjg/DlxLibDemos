namespace DlxLibDemos.Demos.Nonogram;

public record Puzzle(
  int Size,
  HorizontalRunGroup[] HorizontalRunGroups,
  VerticalRunGroup[] VerticalRunGroups
);
