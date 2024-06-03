using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bridge
{
    class Program
    {
        static void Main(string[] args)
        {
            Bridge.Queue<string> arrayQueue = new Bridge.Queue<string>(new ArrayImpl<string>());
            arrayQueue.enQueue("aaa");
            Console.WriteLine(arrayQueue.deQueue());

            Bridge.Queue<string> linkedQueue = new Bridge.Queue<string>(new LinkedListImpl<string>());
            linkedQueue.enQueue("bbb");
            Console.WriteLine(linkedQueue.deQueue());

            Bridge.Stack<string> arrayStack = new Bridge.Stack<string>(new ArrayImpl<string>());
            arrayStack.push("ccc");
            Console.WriteLine(arrayStack.pop());

            Bridge.Stack<String> linkedStack = new Bridge.Stack<String>(new LinkedListImpl<String>());
            linkedStack.push("ddd");
            Console.WriteLine(linkedStack.pop());


            Console.ReadLine();
        }
    }
}
