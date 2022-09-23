using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace InstructionLevelParallelism
{
    public class ILPBenchmark
    {
        [Benchmark]
        public int Slow()
        {
            int s = 0;
            for (int i = 0; i < 100000; i++)
            {
                s++; s++; s++; s++;
            }
            return s;
        }

        [Benchmark]
        public int Fast()
        {
            int a = 0, b = 0, c = 0, d = 0;
            for (int i = 0; i < 100000; i++)
            {
                a++; b++; c++; d++;
            }
            return a + b + c + d;
        }
    }
    internal class Program
    {
        static void Main(string[] args)
        {
            var b = new ILPBenchmark();
            var slow = b.Slow();
            var fast = b.Fast();
            Console.WriteLine($"{slow} == {fast}");

            var summary = BenchmarkRunner.Run<ILPBenchmark>();
        }
    }
}