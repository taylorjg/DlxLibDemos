namespace DlxLibDemos.Demos.RippleEffect;

public record Puzzle(
  string Name,
  int Size,
  int MaxValue,
  Room[] Rooms,
  InitialValue[] InitialValues
);
