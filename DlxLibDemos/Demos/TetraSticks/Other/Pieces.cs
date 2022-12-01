namespace DlxLibDemos.Demos.TetraSticks;

public static class Pieces
{
  private const string F = "H00 H10 V00 V10";
  private const string H = "V00 V10 H10 V11";
  private const string I = "V00 V10 V20 V30 I10 I20 I30";
  private const string J = "V01 V11 V10 H20 I11";
  private const string L = "V00 V10 V20 H30 I10 I20";
  private const string N = "V20 H20 V01 V11 I11";
  private const string O = "H00 V01 H10 V00";
  private const string P = "H00 V01 H10 V10";
  private const string R = "H10 V11 V01 H01";
  private const string T = "H00 H01 V01 V11 I11";
  private const string U = "V00 H10 H11 V02 I11";
  private const string V = "V00 V10 H20 H21 I10 I21";
  private const string W = "V00 H10 V11 H21";
  private const string X = "V01 V11 H10 H11";
  private const string Y = "H10 V01 V11 V21 I21";
  private const string Z = "H00 V01 V11 H21 I11";

  private static Piece ParsePieceDescription(string label, string pieceDescription) {
    var horizontals = new List<Coords>();
    var verticals = new List<Coords>();
    var junctions = new List<Coords>();

    var bits = pieceDescription.Split(" ", StringSplitOptions.TrimEntries);

    foreach (var bit in bits) {
      var type = bit[0];
      var gotRow = int.TryParse(bit[1].ToString(), out int row);
      var gotCol = int.TryParse(bit[2].ToString(), out int col);
      var coords = new Coords(row, col);
      switch (type) {
        case 'H': horizontals.Add(coords); break;
        case 'V': verticals.Add(coords); break;
        case 'I': junctions.Add(coords); break;
      }
    }

    return new Piece(label, horizontals.ToArray(), verticals.ToArray(), junctions.ToArray());
  }

  public static readonly Piece[] ThePieces =
    new[] {
      ParsePieceDescription(nameof(F), F),
      ParsePieceDescription(nameof(H), H),
      ParsePieceDescription(nameof(I), I),
      ParsePieceDescription(nameof(J), J),
      ParsePieceDescription(nameof(L), L),
      ParsePieceDescription(nameof(N), N),
      ParsePieceDescription(nameof(O), O),
      ParsePieceDescription(nameof(P), P),
      ParsePieceDescription(nameof(R), R),
      ParsePieceDescription(nameof(T), T),
      ParsePieceDescription(nameof(U), U),
      ParsePieceDescription(nameof(V), V),
      ParsePieceDescription(nameof(W), W),
      ParsePieceDescription(nameof(X), X),
      ParsePieceDescription(nameof(Y), Y),
      ParsePieceDescription(nameof(Z), Z)
  };
}
