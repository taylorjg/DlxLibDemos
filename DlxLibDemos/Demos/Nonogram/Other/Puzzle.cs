namespace DlxLibDemos.Demos.Nonogram;

public record Puzzle(
  string Name,
  int Size,
  HorizontalRunGroup[] HorizontalRunGroups,
  VerticalRunGroup[] VerticalRunGroups
);
