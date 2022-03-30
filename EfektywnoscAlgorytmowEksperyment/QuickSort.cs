using System;

public class QuickSort
{
    /// <summary>
    /// Sort array using quick sort.
    /// </summary>
    public void Sort(int[] arr, int left, int right)
    {
        Array.Sort(arr, left, right);
    }

    /// <summary>
    /// Sort array using quick sort.
    /// </summary>
    public void Sort(int[] arr)
    {
        Array.Sort(arr, 0, arr.Length - 1);
    }
}
