using System;
using System.Collections.Generic;

namespace Memento
{
    public class Memento
    {
        private string state;

        public Memento(string state)
        {
            this.state = state;
        }

        public string GetState()
        {
            return state;
        }
    }

    public class Originator
    {
        private string state;

        public void SetState(string state)
        {
            this.state = state;
            Console.WriteLine($"State set to: {state}");
        }

        public string GetState()
        {
            return state;
        }

        public Memento CreateMemento()
        {
            return new Memento(state);
        }

        public void SetMemento(Memento memento)
        {
            state = memento.GetState();
            Console.WriteLine($"State restored to: {state}");
        }
    }

    public class Caretaker
    {
        private List<Memento> mementoList = new List<Memento>();

        public void AddMemento(Memento memento)
        {
            mementoList.Add(memento);
        }

        public Memento GetMemento(int index)
        {
            return mementoList[index];
        }
    }
}
