using System;

namespace FactoryMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            ICreator creator = new ConcreteCreator();
            IProduct product1 = creator.FactoryMethod();
            creator.AnOperation();

            ICarFactory carFactory = new HyundaiFactory();
            Car newCar = carFactory.CreateCar("sonata");
            Console.WriteLine(newCar.toString());

            Car myCar = carFactory.ReturnMyCar("Tomas");
            Car hisCar = carFactory.ReturnMyCar("Tomas");
            Console.WriteLine("myCar == hisCar ? -> " + (myCar == hisCar));


            Console.ReadLine();
        }
    }
}
