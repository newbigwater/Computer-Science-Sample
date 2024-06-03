using System;

namespace Adapter
{
    class Program
    {
        static void Main(string[] args)
        {
            // Adapter가 ITarget의 TargetMethod1()와 Adaptee의 MethodA()를 연결
            ITarget target = new Adapter(new Adaptee());
            target.TargetMethod1();

            Console.ReadLine();
        }
    }
}
