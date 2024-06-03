using System;

namespace Observer
{
    public interface IObserver
    {
        void Update();
    }

    public class ConcreteObserver : IObserver
    {
        private string name;
        private ConcreteSubject subject;
        private int observerState;

        public ConcreteObserver(string name, ConcreteSubject subject)
        {
            this.name = name;
            this.subject = subject;
        }

        public void Update()
        {
            observerState = subject.GetState();
            Console.WriteLine($"{name} has updated its state to {observerState}");
        }
    }
}
