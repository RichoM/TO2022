using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;
using System.Diagnostics;

namespace BranchPrediction
{
    public class BPBenchmark
    {
        public int[] data_sorted;
        public int[] data_shuffled;
        Random rng = new Random();

        public BPBenchmark()
        {
            data_sorted = Enumerable.Range(1, 100_000_000).ToArray();
            data_shuffled = Enumerable.Range(1, 100_000_000).ToArray();
            rng.Shuffle(data_shuffled);
        }

        [Params(true, false)]
        public bool Sorted;

        [Benchmark]
        public (int, int) SplitCount()
        {
            var data = Sorted ? data_sorted : data_shuffled;
            var threshold = data.Length / 2;
            return SplitCount(data, threshold);
        }

        public (int, int) SplitCount(int[] data, int threshold)
        {
            int half1 = 0, half2 = 0;
            for (int i = 0; i < data.Length; i++)
            {
                if (data[i] <= threshold)
                {
                    half1++;
                }
                else
                {
                    half2++;
                }
            }
            return (half1, half2);
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            var benchmark = new BPBenchmark();
            var (h1, h2) = benchmark.SplitCount(benchmark.data_sorted, 50_000_000);
            Console.WriteLine($"{h1}, {h2}");
            */

            var summary = BenchmarkRunner.Run<BPBenchmark>();
        }
    }
}