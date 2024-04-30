using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Prototype
{
    class Program
    {
        static void Main(string[] args)
        {
            // 원본 프로토타입 객체 생성
            ConcretePrototype original = new ConcretePrototype { Id = 1, Name = "original" };
            Console.WriteLine($"original Prototype`s id = {original.Id}, name = {original.Name}");
            // 원본을 복제하여 새로운 객체 생성
            ConcretePrototype cloned = (ConcretePrototype)original.Clone();
            Console.WriteLine($"cloned Prototype`s id = {cloned.Id}, name = {cloned.Name}");

            Console.ReadLine();
        }
    }
}
