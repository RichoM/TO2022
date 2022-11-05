namespace DeadlockExample
{
    internal class Program
    {
        static int counter = 0;
        static object lock_1 = new object();
        static object lock_2 = new object();

        static void T1()
        {
            lock (lock_1)
            {
                Thread.Sleep(100);
                lock (lock_2)
                {
                    counter = counter + 1;
                }
            }
        }

        static void T2()
        {
            lock (lock_1)
            {
                Thread.Sleep(100);
                lock (lock_2)
                {
                    counter = counter + 1;
                }
            }
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Va!");

            var t1 = new Thread(T1);
            var t2 = new Thread(T2);

            t1.Start();
            t2.Start();

            t1.Join();
            t2.Join();

            Console.Write(counter);
        }
    }
}