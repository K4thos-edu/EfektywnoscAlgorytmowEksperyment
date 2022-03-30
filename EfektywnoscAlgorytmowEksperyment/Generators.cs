using System;

class Generators
{
    /// <summary>
    /// Populate array randomly.
    /// </summary>
    public int[] GenerateRandom(int size, int minVal, int maxVal)
    {
        Random rand = new Random();

        int[] a = new int[size];
        for (int i = 0; i < size; i++)
        {
            a[i] = rand.Next(minVal, maxVal);
        }
        return a;
    }

    /// <summary>
    /// Populate array with values sorted in ascending order.
    /// </summary>
    public int[] GenerateSorted(int size, int minVal, int maxVal)
    {
        int[] a = GenerateRandom(size, minVal, maxVal);
        Array.Sort(a);
        return a;
    }

    /// <summary>
    /// Populate array with values sorted in descending order.
    /// </summary>
    public int[] GenerateReversed(int size, int minVal, int maxVal)
    {
        int[] a = GenerateSorted(size, minVal, maxVal);
        Array.Reverse(a);
        return a;
    }

    /// <summary>
    /// Populate array with values almost sorted in ascending order. (5% values in different order)
    /// </summary>
    public int[] GenerateAlmostSorted(int size, int minVal, int maxVal)
    {
        int[] a = GenerateSorted(size, minVal, maxVal);
        ShuffleBagSort(a, Math.Max(1, size / 20));
        return a;
    }

    /// <summary>
    /// Populate array with very few unique values. (10% of the array size)
    /// </summary>
    public int[] GenerateFewUnique(int size)
    {
        int[] a = GenerateSorted(size, 0, Math.Max(2, size / 10));
        return a;
    }

    /// <summary>
    /// Shuffle sorted array.
    /// </summary>
    public static T[] ShuffleBagSort<T>(T[] array, int shuffleSize)
    {
        Random rand = new Random();

        for (int i = 0; i < array.Length; i += shuffleSize)
        {
            //Prevents us from getting index out of bounds, while still getting a shuffle of the 
            //last set of un shuffled array, but breaks for loop if the number of unshuffled array is 1
            if (i + shuffleSize > array.Length)
            {
                shuffleSize = array.Length - i;

                if (shuffleSize <= 1) // should never be less than 1, don't think that's possible lol
                    continue;
            }

            if (i % shuffleSize == 0)
            {
                for (int j = i; j < i + shuffleSize; j++)
                {
                    // Pick random element to swap from our small section of the array.
                    int k = rand.Next(i, i + shuffleSize);
                    // Swap.
                    T tmp = array[k];
                    array[k] = array[j];
                    array[j] = tmp;
                }
            }
        }

        return array;
    }
}