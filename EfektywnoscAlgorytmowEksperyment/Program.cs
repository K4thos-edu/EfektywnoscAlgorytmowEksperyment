using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

// Lab. 01. Efektywność algorytmów - eksperyment

namespace EfektywnoscAlgorytmowEksperyment
{
    internal class Program
    {
        public const int ObservationCount = 12;
        public const int ObservationSkip = 2;

        static void Main(string[] args)
        {

            var output = "";

            var dictArraySizes = new Dictionary<string, int>();
            dictArraySizes.Add("mała", 10);
            dictArraySizes.Add("średnia", 1000);
            dictArraySizes.Add("duża", 100000);

            var arrayTypes = new string[5] { "random", "sorted", "reversed", "almost sorted", "few unique" };

            var caseNo = 0;
            foreach (KeyValuePair<string, int> kvp in dictArraySizes)
            {
                for (int i = 0; i < arrayTypes.Length; i++)
                {
                    caseNo++;
                    output += TestCase(caseNo, kvp.Value, kvp.Key, arrayTypes[i]);
                }

            }

            Console.WriteLine(output);
        }

        static string TestCase(int caseNo, int n, string sampleName, string arrayType)
        {
            // Initilize output string
            var output = $"Przypadek {caseNo}: próba {sampleName} (n = {n}), {arrayType}\n";

            // Create Stopwatch
            var stopWatch = new Stopwatch();

            // Load classes
            Generators obGenerators = new Generators();

            InsertionSort obInsertionSort = new InsertionSort();
            MergeSort obMergeSort = new MergeSort();
            QuickSortClassical obQuickSortClassical = new QuickSortClassical();
            QuickSort obQuickSort = new QuickSort();

            // Generate array used for testing Sort algoritms
            var a = new int[n];
            switch (arrayType)
            {
                case "random":
                    a = obGenerators.GenerateRandom(n, 0, 2147483647);
                    break;
                case "sorted":
                    a = obGenerators.GenerateSorted(n, 0, 2147483647);
                    break;
                case "reversed":
                    a = obGenerators.GenerateReversed(n, 0, 2147483647);
                    break;
                case "almost sorted":
                    a = obGenerators.GenerateAlmostSorted(n, 0, 2147483647);
                    break;
                case "few unique":
                    a = obGenerators.GenerateFewUnique(n);
                    break;
            }

            // For each Sort algorithm class
            for (int algorithm = 0; algorithm < 4; algorithm++)
            {
                //Console.WriteLine($"Sort Class: {sortClass}");

                var arrayElapsed = new double[ObservationCount];

                var algorithmName = "";

                // For each observation
                for (int i = 0; i < ObservationCount; i++)
                {
                    // Clone array a to a1
                    var a1 = (int[])a.Clone();

                    // Restart Stopwatch
                    stopWatch.Restart();

                    // Run Sort method from appropariate class
                    switch (algorithm)
                    {
                        case 0:
                            obInsertionSort.Sort(a1);
                            algorithmName = "InsertionSort";
                            break;
                        case 1:
                            obMergeSort.Sort(a1);
                            algorithmName = "MergeSort";
                            break;
                        case 2:
                            obQuickSortClassical.Sort(a1);
                            algorithmName = "QuickSortClassical";
                            break;
                        case 3:
                            obQuickSort.Sort(a1);
                            algorithmName = "QuickSort";
                            break;
                    }

                    // Stop Stopwatch
                    stopWatch.Stop();

                    // Register elapsed time in array
                    arrayElapsed[i] = double.Parse((stopWatch.Elapsed).TotalMilliseconds.ToString());
                }

                // Sort array with elapsed times (so that we can get rid of unusal results at the end)
                Array.Sort(arrayElapsed);

                // Calculate average
                var avg = arrayElapsed.SkipLast(ObservationSkip).Average();
                var avgStr = avg.ToString($"F{5}");

                // Calculate standard deviation
                var std = StandardDeviation(arrayElapsed.SkipLast(ObservationSkip));
                var stdStr = std.ToString($"F{5}");

                // Append raport data
                output += $"* {algorithmName}: t = {avgStr} +/- {stdStr}\n";
            }
            return output + "\n";
        }

        /// <summary>
        /// Calculates the standard deviation of the given sequence.
        /// </summary>
        static double StandardDeviation(IEnumerable<double> sequence)
        {
            double result = 0;

            if (sequence.Any())
            {
                double average = sequence.Average();
                double sum = sequence.Sum(d => Math.Pow(d - average, 2));
                result = Math.Sqrt((sum) / sequence.Count());
            }
            return result;
        }
    }

}
