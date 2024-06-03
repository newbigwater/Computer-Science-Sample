using System;

namespace Iterator
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcreteAggregate<string> aggregate = new ConcreteAggregate<string>();
            aggregate[0] = "Item A";
            aggregate[1] = "Item B";
            aggregate[2] = "Item C";

            IIterator<string> iterator = aggregate.CreateIterator();
            for (string item = iterator.First(); !iterator.IsDone(); item = iterator.Next())
            {
                Console.WriteLine(item);
            }


            Console.ReadLine();
        }
    }
}
