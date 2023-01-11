namespace DlxLibDemos.Demos.Nonogram;

public record Puzzle(
  int Size,
  Run[] HorizontalRuns,
  Run[] VerticalRuns
);
