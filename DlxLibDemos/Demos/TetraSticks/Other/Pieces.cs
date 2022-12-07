namespace DlxLibDemos.Demos.TetraSticks;

public static class Pieces
{
  private const string F = "H00 V00 H10 V10";
  private const string F_POLYLINES = "0,1 0,0 1,0 1,1|1,1 1,0 2,0";

  private const string H = "V00 V10 H10 V11";
  private const string H_POLYLINES = "0,0 1,0 1,1 2,1|2,0 1,0 1,1 2,1";

  private const string I = "V00 V10 V20 V30 I10 I20 I30";
  private const string I_POLYLINES = "0,0 1,0 2,0 3,0 4,0";

  private const string J = "V10 H20 V11 V01 I11";
  private const string J_POLYLINES = "1,0 2,0 2,1 1,1 0,1";

  private const string L = "V00 V10 V20 H30 I10 I20";
  private const string L_POLYLINES = "0,0 1,0 2,0 3,0 3,1";

  private const string N = "V20 H20 V01 V11 I11";
  private const string N_POLYLINES = "3,0 2,0 2,1 1,1 0,1";

  private const string O = "H00 V01 H10 V00";
  private const string O_POLYLINES = "0,0 0,1 1,1 1,0 0,0";

  private const string P = "H00 V01 H10 V10";
  private const string P_POLYLINES = "0,0 0,1 1,1 1,0 2,0";

  private const string R = "H10 V11 V01 H01";
  private const string R_POLYLINES = "1,0 1,1 2,1|1,0 1,1 0,1 0,2";

  private const string T = "H00 H01 V01 V11 I11";
  private const string T_POLYLINES = "0,0 0,1 1,1 2,1|0,2 0,1 1,1 2,1";

  private const string U = "V00 H10 H11 V02 I11";
  private const string U_POLYLINES = "0,0 1,0 1,1 1,2 0,2";

  private const string V = "V00 V10 H20 H21 I10 I21";
  private const string V_POLYLINES = "0,0 1,0 2,0 2,1 2,2";

  private const string W = "V00 H10 V11 H21";
  private const string W_POLYLINES = "0,0 1,0 1,1 2,1 2,2";

  private const string X = "V01 V11 H10 H11";
  private const string X_POLYLINES = "1,0 1,1 0,1|0,1 1,1 1,2|1,2 1,1 2,1|2,1 1,1 1,0";

  private const string Y = "H10 V01 V11 V21 I21";
  private const string Y_POLYLINES = "1,0 1,1 0,1|1,0 1,1 2,1 3,1";

  private const string Z = "H00 V01 V11 H21 I11";
  private const string Z_POLYLINES = "0,0 0,1 1,1 2,1 2,2";

  private static Piece ParsePieceDescription(
    string label,
    string pieceDescription,
    string polyLineDescriptions
  )
  {
    var horizontals = new List<Coords>();
    var verticals = new List<Coords>();
    var junctions = new List<Coords>();
    var polyLines = ParsePolyLineDescriptions(polyLineDescriptions);

    var bits = pieceDescription.Split(" ", StringSplitOptions.TrimEntries);

    foreach (var bit in bits)
    {
      var type = bit[0];
      var gotRow = int.TryParse(bit[1].ToString(), out int row);
      var gotCol = int.TryParse(bit[2].ToString(), out int col);
      var coords = new Coords(row, col);
      switch (type)
      {
        case 'H':
          horizontals.Add(coords);
          break;
        case 'V':
          verticals.Add(coords);
          break;
        case 'I':
          junctions.Add(coords);
          break;
      }
    }

    return new Piece(
      label,
      horizontals.ToArray(),
      verticals.ToArray(),
      junctions.ToArray(),
      polyLines
    );
  }

  private static Coords[][] ParsePolyLineDescriptions(string polyLineDescriptions)
  {
    return polyLineDescriptions
      .Split("|", StringSplitOptions.TrimEntries)
      .Select(ParsePolyLineDescription)
      .ToArray();
  }

  private static Coords[] ParsePolyLineDescription(string polyLineDescription)
  {
    return polyLineDescription
      .Split(" ", StringSplitOptions.TrimEntries)
      .Select(coordsString =>
      {
        var gotRow = int.TryParse(coordsString[0].ToString(), out int row);
        var gotCol = int.TryParse(coordsString[2].ToString(), out int col);
        return new Coords(row, col);
      })
      .ToArray();
  }

  public static readonly Piece[] ThePieces =
    new[] {
      ParsePieceDescription(nameof(F), F, F_POLYLINES),
      ParsePieceDescription(nameof(H), H, H_POLYLINES),
      ParsePieceDescription(nameof(I), I, I_POLYLINES),
      ParsePieceDescription(nameof(J), J, J_POLYLINES),
      ParsePieceDescription(nameof(L), L, L_POLYLINES),
      ParsePieceDescription(nameof(N), N, N_POLYLINES),
      ParsePieceDescription(nameof(O), O, O_POLYLINES),
      ParsePieceDescription(nameof(P), P, P_POLYLINES),
      ParsePieceDescription(nameof(R), R, R_POLYLINES),
      ParsePieceDescription(nameof(T), T, T_POLYLINES),
      ParsePieceDescription(nameof(U), U, U_POLYLINES),
      ParsePieceDescription(nameof(V), V, V_POLYLINES),
      ParsePieceDescription(nameof(W), W, W_POLYLINES),
      ParsePieceDescription(nameof(X), X, X_POLYLINES),
      ParsePieceDescription(nameof(Y), Y, Y_POLYLINES),
      ParsePieceDescription(nameof(Z), Z, Z_POLYLINES)
  };
}
