using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
            //new Program().Start();
            new Program().StartPartitioner();

            Thread.Sleep(5000);
        }

        void Start()
        {
            const int count = 100000;
            var array = new String[count];

            Parallel.For(0, count, i =>
            {
                array[i] = String.Format("{0} on thread {1}", i, Thread.CurrentThread.ManagedThreadId);
            });

            //Parallel.ForEach(array, s =>
            foreach(var s in array) {
            //{
                System.Console.WriteLine(s);
            }
            //});
        }
        void StartPartitioner()
        {
            const int count = 100000;
            var array = new String[count];

            var rangePartitioner = Partitioner.Create(0, count);

            Parallel.ForEach(rangePartitioner, (range, loopState)=>
            {
                for (int i = range.Item1; i < range.Item2; i++ )
                    array[i] = String.Format("{0} on thread {1}", i, Thread.CurrentThread.ManagedThreadId);
            });

            foreach (var s in array)
            {
                //{
                System.Console.WriteLine(s);
            }
        }
    }
}
