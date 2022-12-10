namespace DlxLibDemos.Demos.AztecDiamond;

public enum Orientation { North, South, East, West }

public static class OrientationExtensions
{
  public static Orientation RotateCW(this Orientation orientation)
  {
    switch (orientation)
    {
      case Orientation.North: return Orientation.East;
      case Orientation.East: return Orientation.South;
      case Orientation.South: return Orientation.West;
      default: case Orientation.West: return Orientation.North;
    }
  }
}
