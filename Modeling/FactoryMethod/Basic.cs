using System;

namespace FactoryMethod
{
    public interface ICreator
    {
        IProduct FactoryMethod();
        void AnOperation();
    }

    public interface IProduct
    {
        void Method1();
        void Method2();
    }

    public class ConcreteCreator : ICreator
    {
        public IProduct FactoryMethod()
        {
            return new ConcreteProduct();
        }

        public void AnOperation()
        {
            IProduct product = FactoryMethod();
            product.Method1();
            product.Method2();
        }
    }

    public class ConcreteProduct : IProduct
    {
        public void Method1()
        {
            Console.WriteLine("ConcreteProduct Method1");
        }

        public void Method2()
        {
            Console.WriteLine("ConcreteProduct Method2");
        }
    }
}
