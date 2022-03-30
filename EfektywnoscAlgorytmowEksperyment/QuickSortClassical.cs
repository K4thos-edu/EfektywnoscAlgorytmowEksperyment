using System;

public class QuickSortClassical
{
    /// <summary>
    /// Sort array using classical quick sort.
    /// </summary>
    public void Sort(int[] arr, int left, int right)
    {
        if (left < right)
        {
            int pivot = Partition(arr, left, right);

            if (pivot > 1)
            {
                Sort(arr, left, pivot - 1);
            }
            if (pivot + 1 < right)
            {
                Sort(arr, pivot + 1, right);
            }
        }

    }

    /// <summary>
    /// Sort array using classical quick sort.
    /// </summary>
    public void Sort(int[] arr)
    {
        Array.Sort(arr, 0, arr.Length - 1);
    }

    static int Partition(int[] arr, int left, int right)
    {
        int pivot = arr[left];
        while (true)
        {

            while (arr[left] < pivot)
            {
                left++;
            }

            while (arr[right] > pivot)
            {
                right--;
            }

            if (left < right)
            {
                if (arr[left] == arr[right]) return right;

                int temp = arr[left];
                arr[left] = arr[right];
                arr[right] = temp;


            }
            else
            {
                return right;
            }
        }
    }
}
