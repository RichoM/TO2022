using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace RaceCondition
{
    class Program
    {
        static int counter;
        const int nthreads = 30;
        const int niter = 5_000_000;

        static void Main(string[] args)
        {
            Console.ReadLine();
            Test(nameof(Increment), Increment);
            Test(nameof(IncrementWithLock), IncrementWithLock);
            Test(nameof(IncrementWithInterlocked), IncrementWithInterlocked);
            Console.ReadLine();
        }

        static void Test(string testName, Action testAction)
        {
            counter = 0;
            Console.WriteLine(testName);

            Stopwatch sw = new Stopwatch();
            sw.Start();
            {
                // Creamos unos cuantos threads
                var threads = new List<Thread>();
                for (int i = 0; i < nthreads; i++)
                {
                    // Cada thread va a ejecutar la acción multiples veces
                    threads.Add(new Thread(() =>
                    {
                        for (int j = 0; j < niter; j++)
                        {
                            testAction();
                        }
                    }));
                }

                // Iniciamos todos los threads
                for (int i = 0; i < nthreads; i++)
                {
                    threads[i].Start();
                }

                // Esperamos que todos los threads terminen la ejecución
                for (int i = 0; i < nthreads; i++)
                {
                    threads[i].Join();
                }
            }
            sw.Stop();

            int expected = niter * nthreads;
            Console.WriteLine($"ACTUAL: {counter}, EXPECTED: {expected}");
            Console.WriteLine($"RESULT: {(counter == expected ? "SUCCESS" : "ERROR")}");
            Console.WriteLine($"TIME (ms): {sw.ElapsedMilliseconds}");
            Console.WriteLine();
        }

        static void Increment()
        {
            // Sin ningún mecanismo de sincronización múltiples threads acceden a 
            // esta variable al mismo tiempo. Race condition!
            counter++;
        }

        static object counter_lock = new object();
        static void IncrementWithLock()
        {
            // Usando un lock sólo un thread a la vez puede acceder a la variable. 
            // Ahora el resultado es el correcto pero el proceso tarda mucho más.
            lock (counter_lock)
            {
                counter++;
            }
        }

        static void IncrementWithInterlocked()
        {
            // Usando Interlocked podemos incrementar la variable de forma atómica.
            Interlocked.Increment(ref counter);
        }
    }
}
