using System;

namespace Command
{
    class Program
    {
        static void Main(string[] args)
        {
            Receiver receiver = new Receiver();
            ICommand command = new ConcreteCommand(receiver);

            Invoker invoker = new Invoker();
            invoker.SetCommand(command);
            invoker.ExecuteCommand();


            Console.ReadLine();
        }
    }
}
