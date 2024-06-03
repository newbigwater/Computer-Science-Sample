using System;

namespace ChainOfResponsibility
{
    class Program
    {
        static void Main(string[] args)
        {
            Handler h1 = new ConcreteHandler1();
            Handler h2 = new ConcreteHandler2();

            h1.SetSuccessor(h2);

            int[] requests = { 5, 14, 22, 3, 18, 27, 20 };
            foreach (int request in requests)
            {
                h1.HandleRequest(request);
            }


            Console.ReadLine();
        }
    }
}
