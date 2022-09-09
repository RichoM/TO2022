using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Drawing;
using System.Drawing.Imaging;
using System.Diagnostics;

namespace ImageProcessing
{
    class Program
    {
        static void Main(string[] args)
        {
            Bitmap image = Properties.Resources.lena_4096;
            Color dominant = Color.Transparent;
            TimeSpan time = TimeToRun(() => dominant = image.DominantColor());
            Console.WriteLine(dominant);
            Console.WriteLine();
            Console.WriteLine("Time to run: {0} ms", time.TotalMilliseconds);
            Console.Read();
        }

        static TimeSpan TimeToRun(Action action, int times = 1)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();
            for (int i = 0; i < times; i++) { action(); }
            stopwatch.Stop();
            return new TimeSpan(stopwatch.Elapsed.Ticks / times);
        }

    }
}
