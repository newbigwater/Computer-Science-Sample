using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using KeepAlive;

namespace Garbage_Collection
{
    class Program
    {
        static void Main(string[] args)
        {
            KeepAliveTestClass keepAliveTestObj1 = new KeepAliveTestClass();
            keepAliveTestObj1.MyMethod_Before();

            KeepAliveTestClass keepAliveTestObj2 = new KeepAliveTestClass();
            keepAliveTestObj2.MyMethod_After();

            while (true)
            {
                for(int i = 0; i < 10; i++)
                    Thread.Sleep(100);

                Console.Write($"Input : ");
                string input = Console.ReadLine();
                bool bCheck = int.Parse(input) == 0 ? true : false;
                if (bCheck)
                    break;

                GC.Collect();
            }
        }
    }
}
