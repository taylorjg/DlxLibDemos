namespace DlxLibDemos.Demos.Crossword;

public record Puzzle(
  string Name,
  int Size,
  Coords[] Blocks,
  Dictionary<int, Coords[]> AcrossClues,
  Dictionary<int, Coords[]> DownClues,
  Dictionary<int, string[]> AcrossClueCandidates,
  Dictionary<int, string[]> DownClueCandidates
);
