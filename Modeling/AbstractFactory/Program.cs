using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AbstractFactory
{
    class Program
    {
        static void Main(string[] args)
        {
            // 인터페이스 우선 제공
            IAbstractFactory factory;

            // 원하는 클래스 인스턴스 생성
            factory = new ConcreteFactory1();
            IAbstractProduct productA;
            productA = factory.CreateProduct1();
            productA.insertData("A1");
            productA = factory.CreateProduct2();
            productA.insertData("A2");

            factory = new ConcreteFactory2();
            IAbstractProduct productB;
            productB = factory.CreateProduct1();
            productB.insertData("B1");
            productB = factory.CreateProduct2();
            productB.insertData("B2");

            Console.ReadLine();
        }
    }
}
