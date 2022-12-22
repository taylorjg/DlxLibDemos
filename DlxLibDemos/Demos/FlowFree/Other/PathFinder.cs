namespace DlxLibDemos.Demos.FlowFree;

// https://en.wikipedia.org/wiki/A*_search_algorithm

public class PathFinder
{
  private Puzzle _puzzle;

  public PathFinder(Puzzle puzzle)
  {
    _puzzle = puzzle;
  }

  public Coords[] FindPath(ColourPair colourPair, Coords[] additionalObstacles = null)
  {
    var start = colourPair.Start;
    var goal = colourPair.End;
    var openSet = new HashSet<Coords> { start };
    var cameFrom = new Dictionary<Coords, Coords>();
    var gScore = new Dictionary<Coords, int> { { start, 0 } };
    var fScore = new Dictionary<Coords, int> { { start, Heuristic(start, goal) } };

    while (openSet.Any())
    {
      var current = NodeWithLowestScore(openSet, fScore);

      if (current == goal)
      {
        return ReconstructPath(cameFrom, current);
      }

      openSet.Remove(current);

      foreach (var n in Neighbours(current, goal, additionalObstacles ?? new Coords[0]))
      {
        var tentativeScore = gScore[current] + 1;
        var nScore = gScore.GetValueOrDefault(n, int.MaxValue);
        if (tentativeScore < nScore)
        {
          cameFrom[n] = current;
          gScore[n] = tentativeScore;
          fScore[n] = tentativeScore + Heuristic(n, goal);
          if (!openSet.Contains(n))
          {
            openSet.Add(n);
          }
        }
      }
    }

    return null;
  }

  private static Coords NodeWithLowestScore(HashSet<Coords> openSet, Dictionary<Coords, int> fScore)
  {
    return openSet.MinBy(n => fScore[n]);
  }

  private Coords[] Neighbours(Coords current, Coords goal, Coords[] additionalObstacles)
  {
    var ns = new[] {
      current.Up(),
      current.Down(),
      current.Left(),
      current.Right()
    };

    var isWithinPuzzle = (Coords n) => (
      n.Row >= 0 && n.Row < _puzzle.Size &&
      n.Col >= 0 && n.Col < _puzzle.Size
    );

    var isNotAdditionalObstacle = (Coords n) =>
      !additionalObstacles.Contains(n);

    var isEmptyLocationOrGoal = (Coords n) =>
      _puzzle.EmptyLocations.Contains(n) || n == goal;

    return ns
      .Where(isWithinPuzzle)
      .Where(isNotAdditionalObstacle)
      .Where(isEmptyLocationOrGoal)
      .ToArray();
  }

  private int Heuristic(Coords a, Coords b)
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
