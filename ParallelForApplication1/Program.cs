using System;
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
            new Program().Start();

            Thread.Sleep(10000);
        }

        void Start()
        {
            const int count = 100000;
            var array = new String[count];

            Parallel.For(0, count, i =>
            {
                array[i] = Convert.ToString(i);
            });

            foreach(var s in array) {
                System.Console.WriteLine(s);
            }
        }
    }
}
