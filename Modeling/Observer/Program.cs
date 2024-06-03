using System;

namespace Observer
{
    class Program
    {
        static void Main(string[] args)
        {
            ConcreteSubject subject = new ConcreteSubject();
            ConcreteObserver observer1 = new ConcreteObserver("Observer 1", subject);
            ConcreteObserver observer2 = new ConcreteObserver("Observer 2", subject);

            subject.Attach(observer1);
            subject.Attach(observer2);
            subject.SetState(5);
            subject.SetState(10);

            subject.Detach(observer1);
            subject.SetState(15);


            Console.ReadLine();
        }
    }
}
