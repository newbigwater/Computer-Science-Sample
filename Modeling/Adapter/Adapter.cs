using System;

namespace Adapter
{
    public interface ITarget
    {
        void TargetMethod1();
        void TargetMethod2();
    }

    public class Adaptee
    {
        public void MethodA()
        {
            Console.WriteLine("MethodA called");
        }

        public void MethodB()
        {
            Console.WriteLine("MethodB called");
        }
    }

    public class Adapter : ITarget
    {
        private readonly Adaptee _adaptee;

        public Adapter(Adaptee adaptee)
        {
            _adaptee = adaptee;
        }

        public void TargetMethod1()
        {
            // Adaptee의 MethodA를 호출
            _adaptee.MethodA();
        }

        public void TargetMethod2()
        {
            // Adaptee의 MethodB를 호출
            _adaptee.MethodB();
        }
    }
}
