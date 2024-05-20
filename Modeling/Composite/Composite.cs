using System;
using System.Collections.Generic;

namespace CompositePattern
{
    // 복합 객체(리프와 다른 복합 객체를 포함하는 객체)
    class Composite : IComponent
    {
        private string _name;
        private List<IComponent> _children = new List<IComponent>();

        public Composite(string name)
        {
            _name = name;
        }

        public void Add(IComponent component)
        {
            _children.Add(component);
        }

        public void Remove(IComponent component)
        {
            _children.Remove(component);
        }

        public void Operation(int depth)
        {
            Console.WriteLine(new string('-', depth) + _name);

            foreach (var child in _children)
            {
                child.Operation(depth + 2);
            }
        }
    }
}
