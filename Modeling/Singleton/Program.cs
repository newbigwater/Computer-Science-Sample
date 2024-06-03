using System;

namespace Singleton
{
    class Program
    {
        static void Main(string[] args)
        {
            Singleton singleton1 = Singleton.GetInstance();
            Singleton singleton2 = Singleton.GetInstance();
            if (singleton1 == singleton2) Console.WriteLine("singleton1 and 2 is same !!");
            else Console.WriteLine("singleton1 and singleton2 is not same !!");

            Console.ReadLine();
        }
    }
}
