using System;

namespace Proxy
{
    class Program
    {
        static void Main(string[] args)
        {
            ISubject proxy = new Proxy();
            proxy.Request();


            Console.ReadLine();
        }
    }
}
