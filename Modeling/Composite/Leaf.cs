using System;

namespace CompositePattern
{
    // 리프(단일 객체)
    class Leaf : IComponent
    {
        private string _name;

        public Leaf(string name)
        {
            _name = name;
        }

        public void Operation(int depth)
        {
            Console.WriteLine(new string('-', depth) + _name);
        }
    }
}
