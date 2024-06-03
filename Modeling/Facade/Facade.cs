using System;

namespace Facade
{
    public class Facade
    {
        private SubSystemA subSystemA;
        private SubSystemB subSystemB;
        private SubSystemC subSystemC;

        public Facade(SubSystemA subSystemA, SubSystemB subSystemB, SubSystemC subSystemC)
        {
            this.subSystemA = subSystemA;
            this.subSystemB = subSystemB;
            this.subSystemC = subSystemC;
        }

        public void SubSystemOn()
        {
            subSystemA.On();
            subSystemB.On();
            subSystemC.On();
        }

        public void SubSystemOff()
        {
            subSystemA.Off();
            subSystemB.Off();
            subSystemC.Off();
        }
    }

    public class SubSystemA
    {
        public void On()
        {
            Console.WriteLine("SubSystemA is On");
        }

        public void Off()
        {
            Console.WriteLine("SubSystemA is Off");
        }
    }

    public class SubSystemB
    {
        public void On()
        {
            Console.WriteLine("SubSystemB is On");
        }

        public void Off()
        {
            Console.WriteLine("SubSystemB is Off");
        }
    }

    public class SubSystemC
    {
        public void On()
        {
            Console.WriteLine("SubSystemC is On");
        }

        public void Off()
        {
            Console.WriteLine("SubSystemC is Off");
        }
    }
}
