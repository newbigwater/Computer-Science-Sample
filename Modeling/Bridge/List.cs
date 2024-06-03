using System;

namespace Bridge
{
    public class List<T>
    {
        public AbstractList<T> impl;

        public List(AbstractList<T> list)
        {
            impl = list;
        }

        public void add(T obj)
        {
            impl.addElement(obj);
        }
        public T get(int i)
        {
            return impl.getElement(i);
        }
        public T remove(int i)
        {
            return impl.deleteElement(i);
        }
        public int getSize()
        {
            return impl.getElementSize();
        }
    }

    public class Queue<T> : List<T>
    {
        public Queue(AbstractList<T> list) : base(list)
        {
            Console.WriteLine("Queue를 구현합니다.");
        }

        public void enQueue(T obj)
        {
            impl.addElement(obj);
        }

        public T deQueue()
        {
            return impl.deleteElement(0);
        }
    }

    public class Stack<T> : List<T>
    {
        public Stack(AbstractList<T> list) : base(list)
        {
            Console.WriteLine("Stack을 구현합니다.");
        }

        public void push(T obj)
        {
            impl.insertElement(obj, 0);
        }

        public T pop()
        {
            return impl.deleteElement(0);
        }
    }
}
