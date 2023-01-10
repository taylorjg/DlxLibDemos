namespace DlxLibDemos.Demos.Kakuro;

// https://www.chadgolden.com/blog/finding-all-the-permutations-of-an-array-in-c-sharp

public static class Permutations
{
  public static List<int[]> DoPermute(int[] nums)
  {
    var permutations = new List<int[]>();
    DoPermuteInternal(nums, 0, nums.Length - 1, permutations);
    return permutations;
  }

  private static void DoPermuteInternal(int[] nums, int start, int end, List<int[]> list)
  {
    if (start == end)
    {
      list.Add(nums[..]);
    }
    else
    {
      for (var i = start; i <= end; i++)
      {
        Swap(ref nums[start], ref nums[i]);
        DoPermuteInternal(nums, start + 1, end, list);
        Swap(ref nums[start], ref nums[i]);
      }
    }
  }

  private static void Swap(ref int a, ref int b)
  {
    var temp = a;
    a = b;
    b = temp;
  }
}
