using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ParallelForApplication1
{
    class Program
    {
        static void Main(string[] args)
        {

            for (int i = 0; i < 5; i++)
            {
                new Program().Start();
                new Program().StartPartitioner();
                new Program().StartNormal();
            }

            Thread.Sleep(5000);
        }

        const int count = 500;

        void StartNormal()
        {
            var array = new String[count];

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int i = 0; i < count; i++)
            {
                array[i] = String.Format("{0} on thread {1}", i, Thread.CurrentThread.ManagedThreadId);
            }
            stopWatch.Stop();

            System.Console.WriteLine("Normal   calc time {0}s", stopWatch.Elapsed);
        }

        void Start()
        {
            var array = new String[count];

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            Parallel.For(0, count, i =>
            {
                array[i] = String.Format("{0} on thread {1}", i, Thread.CurrentThread.ManagedThreadId);
            });
            stopWatch.Stop();

            System.Console.WriteLine("Parallel calc time {0}s", stopWatch.Elapsed);

            //Parallel.ForEach(array, s =>
            //foreach(var s in array) {
            //{
            //    System.Console.WriteLine(s);
            //}
            //});

        }
        void StartPartitioner()
        {
            var array = new String[count];

            var rangePartitioner = Partitioner.Create(0, count);

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            Parallel.ForEach(rangePartitioner, (range, loopState)=>
            {
                for (int i = range.Item1; i < range.Item2; i++ )
                    array[i] = String.Format("{0} on thread {1}", i, Thread.CurrentThread.ManagedThreadId);
            });
            stopWatch.Stop();
            // Get the elapsed time as a TimeSpan value.
            //TimeSpan ts = stopWatch.Elapsed;

            System.Console.WriteLine("Range    calc time {0}s", stopWatch.Elapsed);

            //foreach (var s in array)
            //{
            //    System.Console.WriteLine(s);
            //}
        }
    }
}
