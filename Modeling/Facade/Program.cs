using System;

namespace Facade
{
    class Program
    {
        static void Main(string[] args)
        {
            SubSystemA subsystemA = new SubSystemA();
            SubSystemB subsystemB = new SubSystemB();
            SubSystemC subsystemC = new SubSystemC();
            Facade facade = new Facade(subsystemA, subsystemB, subsystemC);
            facade.SubSystemOn();
            facade.SubSystemOff();


            Console.ReadLine();
        }
    }
}
