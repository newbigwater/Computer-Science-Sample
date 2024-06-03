using System;

namespace Flyweight
{
    public abstract class Flyweight
    {
        public abstract void Operation(int extrinsicState);
    }

    public class ConcreteFlyweight : Flyweight
    {
        private string intrinsicState;

        public ConcreteFlyweight(string intrinsicState)
        {
            this.intrinsicState = intrinsicState;
        }

        public override void Operation(int extrinsicState)
        {
            Console.WriteLine($"ConcreteFlyweight: IntrinsicState = {intrinsicState}, ExtrinsicState = {extrinsicState}");
        }
    }

    public class UnsharedConcreteFlyweight : Flyweight
    {
        private string allState;

        public UnsharedConcreteFlyweight(string allState)
        {
            this.allState = allState;
        }

        public override void Operation(int extrinsicState)
        {
            Console.WriteLine($"UnsharedConcreteFlyweight: AllState = {allState}, ExtrinsicState = {extrinsicState}");
        }
    }
}
