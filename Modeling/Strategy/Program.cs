using System;

namespace Strategy
{
    class Program
    {
        static void Main(string[] args)
        {
            iStrategy strategy = null;
            int type = 2;
            if (type == 1) strategy = new ConcreteStrategyA();
            else if (type == 2) strategy = new ConcreteStrategyB();
            else if (type == 3) strategy = new ConcreteStrategyC();
            strategy.AlgorithmInterface();


            Console.ReadLine();
        }
    }
}
