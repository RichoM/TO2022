using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace FalseSharingExample
{
    /*
BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19043.2251/21H1/May2021Update)
AMD Ryzen 7 3700U with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.306
  [Host]     : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.11 (6.0.1122.52304), X64 RyuJIT AVX2


| Method |       Mean |    Error |   StdDev | Ratio |
|------- |-----------:|---------:|---------:|------:|
|      A | 2,721.7 us | 38.19 us | 35.73 us |  1.00 |
|      B |   596.6 us |  8.00 us |  7.09 us |  0.22 |
     */
    public class FalseSharingBenchmark
    {
        int[] values = new int[0];

        [GlobalSetup]
        public void Setup()
        {
            values = Enumerable.Range(0, 50).ToArray();
        }

        [Benchmark(Baseline = true)]
        public void A()  // Single cache line
        {
            var t1 = Task.Run(() => Inc(0));
            var t2 = Task.Run(() => Inc(1));
            var t3 = Task.Run(() => Inc(2));
            var t4 = Task.Run(() => Inc(3));
            Task.WaitAll(t1, t2, t3, t4);
        }

        [Benchmark]
        public void B() // 4 cache lines
        {
            var t1 = Task.Run(() => Inc(0));
            var t2 = Task.Run(() => Inc(16));
            var t3 = Task.Run(() => Inc(32));
            var t4 = Task.Run(() => Inc(48));
            Task.WaitAll(t1, t2, t3, t4);
        }

        void Inc(int index)
        {
            for (int i = 0; i < 100_000; i++)
            {
                values[index]++;
            }
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<FalseSharingBenchmark>();
        }
    }
}