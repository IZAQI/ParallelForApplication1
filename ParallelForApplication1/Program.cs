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
            Thread.Sleep(2000);

            for (int i = 0; i < 5; i++)
            {
                new Program().StartNormal();
                new Program().StartPartitioner();
                new Program().StartParallel();

                System.Console.WriteLine("-----------------------------------------");
            }

            Thread.Sleep(5000);
        }

        const int count = 50000;

        #region count=500 result
        //Normal   calc time 00:00:00.0026005s
        //Range    calc time 00:00:00.0192606s
        //Parallel calc time 00:00:00.0016875s
        //-----------------------------------------
        //Normal   calc time 00:00:00.0004969s
        //Range    calc time 00:00:00.0004075s
        //Parallel calc time 00:00:00.0002972s
        //-----------------------------------------
        //Normal   calc time 00:00:00.0003861s
        //Range    calc time 00:00:00.0002506s
        //Parallel calc time 00:00:00.0002617s
        //-----------------------------------------
        //Normal   calc time 00:00:00.0003823s
        //Range    calc time 00:00:00.0002715s
        //Parallel calc time 00:00:00.0002634s
        //-----------------------------------------
        //Normal   calc time 00:00:00.0003925s
        //Range    calc time 00:00:00.0002668s
        //Parallel calc time 00:00:00.0002407s
        //-----------------------------------------
        #endregion

        #region  count=5000 result
        //Normal   calc time 00:00:00.0092741s
        //Range    calc time 00:00:00.0219373s
        //Parallel calc time 00:00:00.0061560s
        //-----------------------------------------
        //Normal   calc time 00:00:00.0047927s
        //Range    calc time 00:00:00.0026736s
        //Parallel calc time 00:00:00.0030213s
        //-----------------------------------------
        //Normal   calc time 00:00:00.0047863s
        //Range    calc time 00:00:00.0015271s
        //Parallel calc time 00:00:00.0013518s
        //-----------------------------------------
        //Normal   calc time 00:00:00.0020925s
        //Range    calc time 00:00:00.0013064s
        //Parallel calc time 00:00:00.0013210s
        //-----------------------------------------
        //Normal   calc time 00:00:00.0024367s
        //Range    calc time 00:00:00.0013441s
        //Parallel calc time 00:00:00.0013616s
        //-----------------------------------------
        #endregion

        #region  count=50000 result
        //Normal   calc time 00:00:00.0604253s
        //Range    calc time 00:00:00.0352776s
        //Parallel calc time 00:00:00.0260963s
        //-----------------------------------------
        //Normal   calc time 00:00:00.0357168s
        //Range    calc time 00:00:00.0209157s
        //Parallel calc time 00:00:00.0266830s
        //-----------------------------------------
        //Normal   calc time 00:00:00.0335019s
        //Range    calc time 00:00:00.0239221s
        //Parallel calc time 00:00:00.0248804s
        //-----------------------------------------
        //Normal   calc time 00:00:00.0389066s
        //Range    calc time 00:00:00.0255702s
        //Parallel calc time 00:00:00.0394899s
        //-----------------------------------------
        //Normal   calc time 00:00:00.0516734s
        //Range    calc time 00:00:00.0259577s
        //Parallel calc time 00:00:00.0250951s
        //-----------------------------------------
        #endregion

        /// <summary>
        /// Normal for loop.
        /// </summary>
        void StartNormal()
        {
            var array = new String[count];

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            for (int i = 0; i < count; i++)
            {
                new Program();
                array[i] = String.Format("{0} on thread {1}", i, Thread.CurrentThread.ManagedThreadId);
            }
            stopWatch.Stop();

            System.Console.WriteLine("Normal   calc time {0}s", stopWatch.Elapsed);
        }

        /// <summary>
        /// Parallel for loop.
        /// </summary>
        void StartParallel()
        {
            var array = new String[count];

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            Parallel.For(0, count, i =>
            {
                new Program();
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

        /// <summary>
        /// Partitoner for loop.
        /// </summary>
        void StartPartitioner()
        {
            var array = new String[count];

            var rangePartitioner = Partitioner.Create(0, count);

            var stopWatch = new Stopwatch();
            stopWatch.Start();

            Parallel.ForEach(rangePartitioner, (range, loopState)=>
            {
                for (int i = range.Item1; i < range.Item2; i++)
                {
                    new Program();
                    array[i] = String.Format("{0} on thread {1}", i, Thread.CurrentThread.ManagedThreadId);
                }
            });
            stopWatch.Stop();

            System.Console.WriteLine("Range    calc time {0}s", stopWatch.Elapsed);

            //foreach (var s in array)
            //{
            //    System.Console.WriteLine(s);
            //}
        }
    }
}
