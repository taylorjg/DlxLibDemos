namespace DlxLibDemos.Demos.Pentominoes;

// https://en.wikipedia.org/wiki/Pentomino

public static class Pieces
{
  private static readonly string[] F = new string[] {
    " XX",
    "XX ",
    " X "
  };

  private static readonly string[] I = new string[] {
    "X",
    "X",
    "X",
    "X",
    "X"
  };

  private static readonly string[] L = new string[] {
    "X ",
    "X ",
    "X ",
    "XX"
  };

  private static readonly string[] N = new string[] {
    " X",
    " X",
    "XX",
    "X "
  };

  private static readonly string[] P = new string[] {
    "XX",
    "XX",
    "X "
  };

  private static readonly string[] T = new string[] {
    "XXX",
    " X ",
    " X "
  };

  private static readonly string[] U = new string[] {
    "X X",
    "XXX"
  };

  private static readonly string[] V = new string[] {
    "X  ",
    "X  ",
    "XXX"
  };

  private static readonly string[] W = new string[] {
    "X  ",
    "XX ",
    " XX"
  };

  private static readonly string[] X = new string[] {
    " X ",
    "XXX",
    " X "
  };

  private static readonly string[] Y = new string[] {
    " X",
    "XX",
    " X",
    " X"
  };

  private static readonly string[] Z = new string[] {
    "XX ",
    " X ",
    " XX"
  };

  public static readonly Piece[] ThePieces =
    new[] {
      new Piece(nameof(F), F),
      new Piece(nameof(I), I),
      new Piece(nameof(L), L),
      new Piece(nameof(N), N),
      new Piece(nameof(P), P),
      new Piece(nameof(T), T),
      new Piece(nameof(U), U),
      new Piece(nameof(V), V),
      new Piece(nameof(W), W),
      new Piece(nameof(X), X),
      new Piece(nameof(Y), Y),
      new Piece(nameof(Z), Z)
    };
}
