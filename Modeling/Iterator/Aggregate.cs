using System.Collections.Generic;

namespace Iterator
{
    public interface IAggregate<T>
    {
        IIterator<T> CreateIterator();
    }

    public class ConcreteAggregate<T> : IAggregate<T>
    {
        private readonly List<T> _items = new List<T>();

        public IIterator<T> CreateIterator()
        {
            return new ConcreteIterator<T>(this);
        }

        public int Count => _items.Count;

        public T this[int index]
        {
            get => _items[index];
            set => _items.Insert(index, value);
        }
    }
}
