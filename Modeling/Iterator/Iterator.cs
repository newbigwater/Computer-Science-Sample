
namespace Iterator
{
    public interface IIterator<T>
    {
        T First();
        T Next();
        bool IsDone();
        T CurrentItem();
    }

    public class ConcreteIterator<T> : IIterator<T>
    {
        private readonly ConcreteAggregate<T> _aggregate;
        private int _current;

        public ConcreteIterator(ConcreteAggregate<T> aggregate)
        {
            _aggregate = aggregate;
            _current = 0;
        }

        public T First()
        {
            _current = 0;
            return _aggregate[_current];
        }

        public T Next()
        {
            _current++;
            if (!IsDone())
            {
                return _aggregate[_current];
            }
            else
            {
                return default(T);
            }
        }

        public bool IsDone()
        {
            return _current >= _aggregate.Count;
        }

        public T CurrentItem()
        {
            return _aggregate[_current];
        }
    }
}
