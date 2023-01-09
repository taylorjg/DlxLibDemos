namespace DlxLibDemos.Demos.Kakuro;

public record Puzzle(int Size, Coords[] Blocks, Clue[] Clues, Coords[] Unknowns);
