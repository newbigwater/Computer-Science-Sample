using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Decorator
{
    class Program
    {
        static void Main(string[] args)
        {
            IComponent component = new ConcreteComponent();
            Console.WriteLine("Base Component:");
            component.Operation();

            IComponent decoratorA = new ConcreteDecoratorA(component);
            Console.WriteLine("\nComponent with ConcreteDecoratorA:");
            decoratorA.Operation();

            IComponent decoratorB = new ConcreteDecoratorB(component);
            Console.WriteLine("\nComponent with ConcreteDecoratorB:");
            decoratorB.Operation();

            IComponent decoratorAB = new ConcreteDecoratorB(decoratorA);
            Console.WriteLine("\nComponent with ConcreteDecoratorA and ConcreteDecoratorB:");
            decoratorAB.Operation();


            Console.ReadLine();
        }
    }
}
