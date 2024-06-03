using System;

namespace State
{
    class Program
    {
        static void Main(string[] args)
        {
            State.Context context = new State.Context(new ConcreteStateA());
            context.Request();
            context.Request();


            Console.ReadLine();
        }
    }
}
