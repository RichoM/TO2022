using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace LoopUnrolling
{
    public class LUBenchmark
    {
        int[] data = Enumerable.Range(1, 500_000_000).ToArray();
        
        public int SumSlow(int[] data)
        {
            int sum = 0;
            for (int i = 0; i < data.Length; i++)
            {
                sum += data[i];
            }
            return sum;
        }

        public int SumFast(int[] data)
        {
            int sum1 = 0, sum2 = 0, sum3 = 0, sum4 = 0;
            int i = 0;
            for (; i < data.Length - 4; i += 4)
            {
                sum1 += data[i];
                sum2 += data[i + 1];
                sum3 += data[i + 2];
                sum4 += data[i + 3];
            }
            for (; i < data.Length; i++)
            {
                sum1 += data[i];
            }
            return sum1 + sum2 + sum3 + sum4;
        }

        [Benchmark]
        public int SumSlow()
        {
            return SumSlow(data);
        }

        [Benchmark]
        public int SumFast()
        {
            return SumFast(data);
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            /*
            Console.WriteLine("Hello, World!");
            var benchmark = new LUBenchmark();
            var data = Enumerable.Range(1, 500_001).ToArray();
            var slow = benchmark.SumSlow(data);
            var fast = benchmark.SumFast(data);
            Console.WriteLine($"Slow: {slow}");
            Console.WriteLine($"Fast: {fast}");
            Console.WriteLine($"Correct? {slow == fast}");
            */
            
            var summary = BenchmarkRunner.Run<LUBenchmark>();
        }
    }
}