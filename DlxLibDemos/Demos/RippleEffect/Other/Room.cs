namespace DlxLibDemos.Demos.RippleEffect;

public record Room(
  string Label,
  Coords[] Cells,
  InitialValue[] InitialValues,
  int StartIndex
);
