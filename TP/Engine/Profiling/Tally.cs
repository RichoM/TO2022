using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Engine.Profiling
{
    public class Tally
    {
        private Stopwatch stopwatch = new Stopwatch();

        private List<long> instanceCounter = new List<long>();
        private List<long> updateEvents = new List<long>();
        private List<long> drawEvents = new List<long>();
        
        public Tally()
        {
            stopwatch.Start();
        }

        public int Count { get { return updateEvents.Count; } }

        public List<long> InstanceCounter { get { return instanceCounter; } }
        public List<long> UpdateEvents { get { return updateEvents; } }
        
        public List<long> DrawEvents { get { return drawEvents; } }

        public List<long> UpdateDurations { get { return CalculateDurations(updateEvents); } }
        public List<long> DrawDurations { get { return CalculateDurations(drawEvents); } }

        public double AverageUpdate { get { return CalculateAverage(UpdateDurations); } }
        public double AverageDraw { get { return CalculateAverage(DrawDurations); } }

        public double MedianUpdate { get { return CalculateMedian(UpdateDurations); } }
        public double MedianDraw { get { return CalculateMedian(DrawDurations); } }

        public long MaxUpdate { get { return CalculateMax(UpdateDurations); } }
        public long MaxDraw { get { return CalculateMax(DrawDurations); } }


        internal void RegisterInstances(long instances)
        {
            instanceCounter.Add(instances);
        }

        internal void RegisterUpdate()
        {
            updateEvents.Add(stopwatch.ElapsedMilliseconds);
        }

        internal void RegisterDraw()
        {
            drawEvents.Add(stopwatch.ElapsedMilliseconds);
        }

        private List<long> CalculateDurations(List<long> events)
        {
            List<long> result = new List<long>();
            for (int i = 1; i < events.Count; i++)
            {
                long first = events[i - 1];
                long second = events[i];
                result.Add(second - first);
            }
            return result;
        }

        private double CalculateAverage(List<long> list)
        {
            if (list.Count == 0) return 0;
            else return list.Average();
        }

        private double CalculateMedian(List<long> list)
        {
            if (list.Count == 0) return 0;
            list.Sort();
            int length = list.Count;
            int half = length / 2;
            if (length % 2 == 0)
            {
                long a = list[half - 1];
                long b = list[half];
                return (a + b) / 2;
            }
            else
            {
                return list[half];
            }            
        }

        private long CalculateMax(List<long> list)
        {
            if (list.Count == 0) return 0;
            return list.Max();
        }

        internal void WriteToFile()
        {
            string fileName;
            int index = 0;
            do
            {
                fileName = string.Format("tally.{0}.{1}.csv", DateTime.Now.ToString("yyyyMMdd"), ++index);
            }
            while (File.Exists(fileName));
            WriteToFile(fileName);
        }

        internal void WriteToFile(string fileName)
        {
            using (StreamWriter writer = new StreamWriter(fileName, false))
            {
                writer.WriteLine("frame,update,draw,instances");
                for (int i = 0; i < Count; i++)
                {
                    writer.WriteLine(string.Format("{0},{1},{2},{3}",
                        i,
                        UpdateEvents.ElementAtOrDefault(i),
                        DrawEvents.ElementAtOrDefault(i),
                        InstanceCounter.ElementAtOrDefault(i)));
                }
                writer.Flush();
            }
        }
    }
}
