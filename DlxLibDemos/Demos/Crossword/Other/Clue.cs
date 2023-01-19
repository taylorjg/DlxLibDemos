namespace DlxLibDemos.Demos.Crossword;

public record Clue(
  ClueType ClueType,
  int ClueNumber,
  Coords[] CoordsList,
  string[] Candidates
);
