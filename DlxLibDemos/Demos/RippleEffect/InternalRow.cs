namespace DlxLibDemos.Demos.RippleEffect;

public record RippleEffectInternalRow(
  Puzzle Puzzle,
  Coords Cell,
  int Value,
  bool IsInitialValue,
  Room Room
);
