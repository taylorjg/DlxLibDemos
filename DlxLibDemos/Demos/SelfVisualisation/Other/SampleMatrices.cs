namespace DlxLibDemos.Demos.SelfVisualisation;

public static class SampleMatrices
{
  public static SampleMatrix[] TheSampleMatrices = new[] {
    new SampleMatrix(
      "Sample Matrix 1",
      new[,] {
        {0, 0, 1, 0, 1, 1, 0},
        {1, 0, 0, 1, 0, 0, 1},
        {0, 1, 1, 0, 0, 1, 0},
        {1, 0, 0, 1, 0, 0, 0},
        {0, 1, 0, 0, 0, 0, 1},
        {0, 0, 0, 1, 1, 0, 1}
      }
    ),
    new SampleMatrix(
      "Sample Matrix 2",
      new[,] {
        {1, 0, 0, 0},
        {0, 1, 1, 0},
        {1, 0, 0, 1},
        {0, 0, 1, 1},
        {0, 1, 0, 0},
        {0, 0, 1, 0}
      }
    )
  };
}
