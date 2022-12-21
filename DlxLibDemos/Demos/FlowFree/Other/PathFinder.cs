namespace DlxLibDemos.Demos.FlowFree;

// https://en.wikipedia.org/wiki/A*_search_algorithm

public class PathFinder
{
  private Puzzle _puzzle;
  private Coords[] _dots;

  public PathFinder(Puzzle puzzle)
  {
    _puzzle = puzzle;
    _dots = _puzzle.ColourPairs
      .SelectMany(colourPair => new[] { colourPair.Start, colourPair.End })
      .ToArray();
  }

  public Coords[] FindPath(ColourPair colourPair)
  {
    var start = colourPair.Start;
    var goal = colourPair.End;
    var openSet = new HashSet<Coords> { start };
    var cameFrom = new Dictionary<Coords, Coords>();
    var gScore = new Dictionary<Coords, double> { { start, 0 } };
    var fScore = new Dictionary<Coords, double> { { start, Heuristic(start, goal) } };

    while (openSet.Any())
    {
      var current = NodeWithLowestScore(openSet, fScore);

      if (current == goal)
      {
        return ReconstructPath(cameFrom, current);
      }

      openSet.Remove(current);

      foreach (var n in Neighbours(current, goal))
      {
        var tentativeScore = gScore[current] + 1;
        var nScore = gScore.GetValueOrDefault(n, double.MaxValue);
        if (tentativeScore < nScore) {
          cameFrom[n] = current;
          gScore[n] = tentativeScore;
          fScore[n] = tentativeScore + Heuristic(n, goal);
          if (!openSet.Contains(n)) {
            openSet.Add(n);
          }
        }
      }
    }

    return null;
  }

  private static Coords NodeWithLowestScore(HashSet<Coords> openSet, Dictionary<Coords, double> fScore)
  {
    return openSet.MinBy(n => fScore[n]);
  }

  private Coords[] Neighbours(Coords current, Coords goal)
  {
    var ns = new[] {
      current.Up(),
      current.Down(),
      current.Left(),
      current.Right()
    };

    return ns
      .Where(n => IsWithinPuzzle(n))
      .Where(n => IsEmptyLocationOrGoal(n, goal))
      .ToArray();
  }

  private bool IsWithinPuzzle(Coords n)
  {
    return (
      n.Row >= 0 &&
      n.Row < _puzzle.Size &&
      n.Col >= 0 &&
      n.Col < _puzzle.Size
    );
  }

  private bool IsEmptyLocationOrGoal(Coords n, Coords goal)
  {
    return !_dots.Contains(n) || n == goal;
  }

  private double Heuristic(Coords a, Coords b)
  {
    var rowDiff = Math.Abs(a.Row - b.Row);
    var colDiff = Math.Abs(a.Col - b.Col);
    return rowDiff + colDiff;
  }

  private static Coords[] ReconstructPath(Dictionary<Coords, Coords> cameFrom, Coords current)
  {
    var totalPath = new List<Coords> { current };

    for (; ; )
    {
      if (!cameFrom.TryGetValue(current, out current)) break;
      totalPath.Add(current);
    }

    totalPath.Reverse();

    return totalPath.ToArray();
  }
}
