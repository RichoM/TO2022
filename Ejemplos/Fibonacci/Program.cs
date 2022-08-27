using System.Diagnostics;
using BenchmarkDotNet.Running;
using BenchmarkDotNet.Attributes;

namespace Fibonacci
{
    // 0, 1, 1, 2, 3, 5, 8, 13, ...

    // Iterativo
    public class FibonacciIterative
    {
        public long Get(long n)
        {
            if (n <= 1) return n;
            long prev2 = 0;
            long prev1 = 1;
            for (long i = 1; i < n; i++)
            {
                var cur = prev2 + prev1;
                prev2 = prev1;
                prev1 = cur;
            }
            return prev1;
        } 
    }
    
    // Memoization
    public class FibonacciMemoization
    {
        Dictionary<long, long> calculatedValues = new Dictionary<long, long>();
        
        public long Get(long n)
        {
            if (n <= 1) return n;

            long result;
            if (calculatedValues.TryGetValue(n, out result))
            {
                return result;
            }

            result = Get(n - 1) + Get(n - 2);
            calculatedValues[n] = result;
            return result;
        }
    }

    public class FibonacciRecursive
    {
        public long Get(long n)
        {
            if (n <= 1) return n;
            return Get(n - 1) + Get(n - 2);
        }
    }

    public class Benchmarks
    {
        [Benchmark]
        public long FibIterative()
        {
            var fibonacci = new FibonacciIterative();
            return fibonacci.Get(30);
        }

        [Benchmark]
        public long FibRecursive()
        {
            var fibonacci = new FibonacciRecursive();
            return fibonacci.Get(30);
        }

        [Benchmark]
        public long FibMemoization()
        {
            var fibonacci = new FibonacciMemoization();
            return fibonacci.Get(30);
        }
    }

    internal class Program
    {
        static void Main_benchmarks(string[] args)
        {
            BenchmarkRunner.Run(typeof(Program).Assembly);
        }

        static void Main_stopwatch(string[] args)
        {
            var fib = new FibonacciRecursive();
            var sw = new Stopwatch();
            sw.Start();
            long resultado = 0;
            for (int i = 0; i < 10; i++)
            {
                resultado += fib.Get(40);
            }
            sw.Stop();
            Console.WriteLine(resultado);
            Console.WriteLine($"Time: {sw.ElapsedMilliseconds/10.0} ms");
        }

        static void Main(string[] args)
        {
            var fib = new FibonacciIterative();
            for (int i = 0; i < 20; i++)
            {
                if (i != 0) { Console.Write(", "); }
                Console.Write(fib.Get(i));
            }
            Console.WriteLine();

            while (true)
            {
                try
                {
                    Console.Write("Enter n: ");
                    var input = Console.ReadLine();
                    var n = long.Parse(input ?? "");
                    var f = fib.Get(n);
                    Console.Write($"fib({n}) = {f}");
                    Console.WriteLine();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
        }
    }
}