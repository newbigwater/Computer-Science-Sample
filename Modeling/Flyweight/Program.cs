using System;

namespace Flyweight
{
    class Program
    {
        static void Main(string[] args)
        {
            FlyweightFactory flyweightFactory = new FlyweightFactory();

            Flyweight fw1 = flyweightFactory.GetFlyweight("A");
            fw1.Operation(1);
            Flyweight fw2 = flyweightFactory.GetFlyweight("B");
            fw2.Operation(2);
            Flyweight fw3 = flyweightFactory.GetFlyweight("A");
            fw3.Operation(3);
            Flyweight unshared = new UnsharedConcreteFlyweight("Unshared State");
            unshared.Operation(4);


            Console.ReadLine();
        }
    }
}
