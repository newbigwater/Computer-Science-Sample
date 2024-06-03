using System;

namespace Strategy
{
    public class ConcreteStrategyA : iStrategy
    {
        public void AlgorithmInterface()
        {
            Console.WriteLine("알고리즘 A 채택");
        }
    }

    public class ConcreteStrategyB : iStrategy
    {
        public void AlgorithmInterface()
        {
            Console.WriteLine("알고리즘 B 채택");
        }
    }

    public class ConcreteStrategyC : iStrategy
    {
        public void AlgorithmInterface()
        {
            Console.WriteLine("알고리즘 C 채택");
        }
    }
}
