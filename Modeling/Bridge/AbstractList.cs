using System;
using System.Collections.Generic;
using System.Linq;

namespace Bridge
{
    public interface AbstractList<T>
    {
        void addElement(T obj);
        T deleteElement(int i);
        int insertElement(T obj, int i);
        T getElement(int i);
        int getElementSize();
    }

    public class ArrayImpl<T> : AbstractList<T>
    {
        System.Collections.Generic.List<T> array;

        public ArrayImpl()
        {
            array = new System.Collections.Generic.List<T>();
            Console.WriteLine("Array로 구현합니다.");
        }

        public void addElement(T obj)
        {
            array.Add(obj);
        }

        public T deleteElement(int i)
        {
            T element = array.ElementAt(i);
            array.RemoveAt(i);

            return element;
        }

        public int insertElement(T obj, int i)
        {
            array.Insert(i, obj);
            return i;
        }

        public T getElement(int i)
        {
            return array.ElementAt(i);
        }

        public int getElementSize()
        {
            return array.Count;
        }
    }

    public class LinkedListImpl<T> : AbstractList<T>
    {
        LinkedList<T> linkedList;

        public LinkedListImpl()
        {
            linkedList = new LinkedList<T>();
            Console.WriteLine("LinkedList로 구현합니다.");
        }

        public void addElement(T obj)
        {
            linkedList.AddLast(obj);
        }

        public T deleteElement(int i)
        {
            T element = linkedList.ElementAt(i);
            linkedList.Remove(element);
            return element;
        }

        public int insertElement(T obj, int i)
        {
            AddAt(i, obj);
            return i;
        }

        public T getElement(int i)
        {
            return linkedList.ElementAt(i);
        }

        public int getElementSize()
        {
            return linkedList.Count;
        }

        private void AddAt(int i, T value)
        {
            if (i < 0 || i > linkedList.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(i));
            }

            if (i == linkedList.Count)
            {
                linkedList.AddLast(value);
            }
            else
            {
                LinkedListNode<T> current = linkedList.First;
                for (int j = 0; j < i; j++)
                {
                    current = current.Next;
                }
                linkedList.AddBefore(current, value);
            }
        }

        private void RemoveAt(int i)
        {
            if (i < 0 || i >= linkedList.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(i));
            }

            LinkedListNode<T> current = linkedList.First;
            for (int j = 0; j < i; j++)
            {
                current = current.Next;
            }

            linkedList.Remove(current);
        }
    }
}
