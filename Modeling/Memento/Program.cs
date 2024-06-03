using System;

namespace Memento
{
    class Program
    {
        static void Main(string[] args)
        {
            Originator originator = new Originator();
            Caretaker caretaker = new Caretaker();
            originator.SetState("State1");
            caretaker.AddMemento(originator.CreateMemento());
            originator.SetState("State2");
            caretaker.AddMemento(originator.CreateMemento());
            originator.SetState("State3");

            originator.SetMemento(caretaker.GetMemento(1));
            originator.SetMemento(caretaker.GetMemento(0));


            Console.ReadLine();
        }
    }
}
