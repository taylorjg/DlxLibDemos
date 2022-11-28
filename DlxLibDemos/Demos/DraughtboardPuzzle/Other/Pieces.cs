namespace DlxLibDemos.Demos.DraughtboardPuzzle;

public static class Pieces
{
  private static readonly string[] A = new string[] {
    "B ",
    "WB",
    "B ",
    "W "
  };

  private static readonly string[] B = new string[] {
    "B  ",
    "WBW"
  };

  private static readonly string[] C = new string[] {
    "W ",
    "BW"
  };

  private static readonly string[] D = new string[] {
    " WB",
    " B ",
    "BW "
  };

  private static readonly string[] E = new string[] {
    "W ",
    "BW",
    " B",
    " W"
  };

  private static readonly string[] F = new string[] {
    "WB ",
    " W ",
    " BW"
  };

  private static readonly string[] G = new string[] {
    "WB ",
    " WB"
  };

  private static readonly string[] H = new string[] {
    "B ",
    "WB",
    "B "
  };

  private static readonly string[] I = new string[] {
    "B ",
    "W ",
    "BW",
    "W "
  };

  private static readonly string[] J = new string[] {
    " B",
    " W",
    " B",
    "BW"
  };

  private static readonly string[] K = new string[] {
    "  W",
    " WB",
    "WB "
  };

  private static readonly string[] L = new string[] {
    "B ",
    "W ",
    "BW"
  };

  private static readonly string[] M = new string[] {
    " B",
    " W",
    "WB",
    "B "
  };

  private static readonly string[] N = new string[] {
    "W ",
    "B ",
    "W ",
    "BW"
  };

  public static readonly Piece[] ThePieces =
    new[] {
      new Piece(nameof(A), A),
      new Piece(nameof(B), B),
      new Piece(nameof(C), C),
      new Piece(nameof(D), D),
      new Piece(nameof(E), E),
      new Piece(nameof(F), F),
      new Piece(nameof(G), G),
      new Piece(nameof(H), H),
      new Piece(nameof(I), I),
      new Piece(nameof(J), J),
      new Piece(nameof(K), K),
      new Piece(nameof(L), L),
      new Piece(nameof(M), M),
      new Piece(nameof(N), N)
    };
}
