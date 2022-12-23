namespace DlxLibDemos.Demos.FlowFree;

public class PathFinder
{
  private Puzzle _puzzle;

  public PathFinder(Puzzle puzzle)
  {
    _puzzle = puzzle;
  }

  public List<Coords[]> FindPaths(ColourPair colourPair)
  {
    var start = colourPair.Start;
    var goal = colourPair.End;

    var currentPath = new Stack<Coords>();
    currentPath.Push(start);

    var paths = new List<Coords[]>();

    var maxDirectionChanges = _puzzle.ColourPairs.Length;

    FindPathsInternal(currentPath, paths, start, goal, maxDirectionChanges);

    return paths;
  }

  // Inspired by this: https://stackoverflow.com/a/22464491
  private void FindPathsInternal(
    Stack<Coords> currentPath,
    List<Coords[]> paths,
    Coords node,
    Coords goal,
    int maxDirectionChanges
  )
  {
    foreach (var nextNode in Neighbours(node, goal))
    {
      if (nextNode == goal)
      {
        var list = new List<Coords> { nextNode };
        list.AddRange(currentPath);
        if (CountDirectionChanges(list) <= maxDirectionChanges)
        {
          paths.Add(list.ToArray());
        }
      }
      else
      {
        if (!currentPath.Contains(nextNode))
        {
          currentPath.Push(nextNode);
          var numDirectionChanges = CountDirectionChanges(currentPath.ToList());
          if (numDirectionChanges <= maxDirectionChanges)
          {
            FindPathsInternal(currentPath, paths, nextNode, goal, maxDirectionChanges);
          }
          currentPath.Pop();
        }
      }
    }
  }

  private Coords[] Neighbours(Coords node, Coords goal)
  {
    var ns = new[] {
      node.Up(),
      node.Down(),
      node.Left(),
      node.Right()
    };

    var isWithinPuzzle = (Coords n) => (
      n.Row >= 0 && n.Row < _puzzle.Size &&
      n.Col >= 0 && n.Col < _puzzle.Size
    );

    var isEmptyLocationOrGoal = (Coords n) =>
      !_puzzle.Dots.Contains(n) || n == goal;

    return ns
      .Where(isWithinPuzzle)
      .Where(isEmptyLocationOrGoal)
      .ToArray();
  }

  private int CountDirectionChanges(List<Coords> path)
  {
    if (path.Count < 3) return 0;
    var count = 0;
    var indices = Enumerable.Range(0, path.Count).ToArray();
    foreach (var index in indices[1..^1])
    {
      var p1 = path[index - 1];
      var p3 = path[index + 1];
      var rowDiff = Math.Abs(p3.Row - p1.Row);
      var colDiff = Math.Abs(p3.Col - p1.Col);
      if (rowDiff != 0 && colDiff != 0) count++;
    }
    return count;
  }
}
