using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Running;

namespace VirtualCallsExample
{
/*
BenchmarkDotNet=v0.13.2, OS=Windows 10 (10.0.19043.2130/21H1/May2021Update)
AMD Ryzen 7 3700U with Radeon Vega Mobile Gfx, 1 CPU, 8 logical and 4 physical cores
.NET SDK=6.0.305
  [Host]     : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT AVX2
  DefaultJob : .NET 6.0.10 (6.0.1022.47605), X64 RyuJIT AVX2

|                       Method |       Mean |    Error |    StdDev |     Median | Ratio | RatioSD |
|----------------------------- |-----------:|---------:|----------:|-----------:|------:|--------:|
|                  SubB_Update |   243.2 us |  4.86 us |  11.36 us |   240.1 us |  1.00 |    0.00 |
|           SubB_UpdateVirtual |   406.0 us |  8.08 us |  17.90 us |   393.5 us |  1.67 |    0.10 |
|             Base_SubA_Update |   241.3 us |  4.81 us |  10.66 us |   240.3 us |  0.99 |    0.06 |
|      Base_SubA_UpdateVirtual |   342.4 us |  5.29 us |   4.41 us |   342.7 us |  1.40 |    0.08 |
|           Base_Random_Update |   336.4 us | 15.48 us |  45.40 us |   341.8 us |  1.38 |    0.23 |
|    Base_Random_UpdateVirtual | 1,136.6 us | 22.55 us |  30.10 us | 1,139.5 us |  4.70 |    0.26 |
|          IBase_Random_Update |   548.4 us | 12.83 us |  37.63 us |   542.3 us |  2.30 |    0.18 |
|   IBase_Random_UpdateVirtual | 1,137.5 us | 21.20 us |  37.13 us | 1,120.7 us |  4.70 |    0.26 |
|            IBase_SubB_Update |   527.4 us |  9.53 us |   8.92 us |   524.3 us |  2.17 |    0.11 |
|     IBase_SubB_UpdateVirtual |   541.5 us | 10.71 us |  23.27 us |   530.5 us |  2.23 |    0.12 |
|        Dynamic_Random_Update | 4,879.9 us | 96.04 us | 202.59 us | 4,765.9 us | 20.12 |    1.18 |
| Dynamic_Random_UpdateVirtual | 4,654.8 us | 92.82 us | 257.21 us | 4,604.7 us | 19.34 |    1.42 |
|          Dynamic_SubC_Update |   551.2 us | 10.97 us |  18.92 us |   542.6 us |  2.27 |    0.11 |
|   Dynamic_SubC_UpdateVirtual |   580.3 us | 11.53 us |  29.97 us |   570.0 us |  2.39 |    0.17 |
*/
    public class PICBenchmarks
    {
        static Random rng = new Random();

        SubB[] subB_objects = new SubB[0];
        Base[] base_random_objects = new Base[0];
        Base[] base_subA_objects = new Base[0];
        IBase[] ibase_random_objects = new IBase[0];
        IBase[] ibase_subB_objects = new IBase[0];
        dynamic[] dynamic_random_objects = new dynamic[0];
        dynamic[] dynamic_subC_objects = new dynamic[0];


        [GlobalSetup]
        public void Setup()
        {
            var range = Enumerable.Range(0, 100_000);
            subB_objects = range.Select(_ => new SubB()).ToArray();

            Base BaseRandomObject(int _)
            {
                var n = rng.Next(0, 3);
                switch (n)
                {
                    case 0: return new SubA();
                    case 1: return new SubB();
                    case 2: return new SubC();
                }
                throw new Exception("Execution should not reach here!");
            }
            base_random_objects = range.Select(BaseRandomObject).ToArray();
            base_subA_objects = range.Select(_ => new SubA()).ToArray();

            IBase IBaseRandomObject(int _)
            {
                return rng.NextDouble() < 0.5 ? new SubB() : new SubC();
            }
            ibase_random_objects = range.Select(IBaseRandomObject).ToArray();
            ibase_subB_objects = range.Select(_ => new SubB()).ToArray();

            dynamic_random_objects = range.Select(BaseRandomObject).ToArray();
            dynamic_subC_objects = range.Select(_ => new SubC()).ToArray();
        }


        [Benchmark(Baseline = true)]
        public void SubB_Update()
        {
            foreach (var obj in subB_objects)
            {
                obj.Update();
            }
        }

        [Benchmark]
        public void SubB_UpdateVirtual()
        {
            foreach (var obj in subB_objects)
            {
                obj.UpdateVirtual();
            }
        }

        [Benchmark]
        public void Base_SubA_Update()
        {
            foreach (var obj in base_subA_objects)
            {
                obj.Update();                    
            }
        }

        [Benchmark]
        public void Base_SubA_UpdateVirtual()
        {
            foreach (var obj in base_subA_objects)
            {
                obj.UpdateVirtual();
            }
        }

        [Benchmark]
        public void Base_Random_Update()
        {
            foreach (var obj in base_random_objects)
            {
                obj.Update();
            }
        }

        [Benchmark]
        public void Base_Random_UpdateVirtual()
        {
            foreach (var obj in base_random_objects)
            {
                obj.UpdateVirtual();
            }
        }

        [Benchmark]
        public void IBase_Random_Update()
        {
            foreach (var obj in ibase_random_objects)
            {
                obj.Update();
            }
        }

        [Benchmark]
        public void IBase_Random_UpdateVirtual()
        {
            foreach (var obj in ibase_random_objects)
            {
                obj.UpdateVirtual();
            }
        }

        [Benchmark]
        public void IBase_SubB_Update()
        {
            foreach (var obj in ibase_subB_objects)
            {
                obj.Update();
            }
        }

        [Benchmark]
        public void IBase_SubB_UpdateVirtual()
        {
            foreach (var obj in ibase_subB_objects)
            {
                obj.UpdateVirtual();
            }
        }

        [Benchmark]
        public void Dynamic_Random_Update()
        {
            foreach (var obj in dynamic_random_objects)
            {
                obj.Update();
            }
        }

        [Benchmark]
        public void Dynamic_Random_UpdateVirtual()
        {
            foreach (var obj in dynamic_random_objects)
            {
                obj.UpdateVirtual();
            }
        }

        [Benchmark]
        public void Dynamic_SubC_Update()
        {
            foreach (var obj in dynamic_subC_objects)
            {
                obj.Update();
            }
        }

        [Benchmark]
        public void Dynamic_SubC_UpdateVirtual()
        {
            foreach (var obj in dynamic_subC_objects)
            {
                obj.UpdateVirtual();
            }
        }
    }
    internal class Program
    {
        static Random rng = new Random();
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<PICBenchmarks>();
        }
    }
}