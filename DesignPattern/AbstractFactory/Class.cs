using System;

namespace AbstractFactory
{
    public class ConcreteFactory1 : IAbstractFactory
    {
        public IAbstractProduct CreateProduct1()
        {
            return new Product1();
        }

        public IAbstractProduct CreateProduct2()
        {
            return new Product2();
        }
    }

    public class ConcreteFactory2 : IAbstractFactory
    {
        public IAbstractProduct CreateProduct1()
        {
            return new Product1();
        }

        public IAbstractProduct CreateProduct2()
        {
            return new Product2();
        }
    }

    public class Product1 : IAbstractProduct
    {
        public void insertData(string data)
        {
            Console.WriteLine($"insert into Product1 data = {data}");
        }

        public void updateData(string data)
        {
            Console.WriteLine($"update into Product1 data = {data}");
        }

        public void deleteData(string data)
        {
            Console.WriteLine($"delete into Product1 data = {data}");
        }
    }

    public class Product2 : IAbstractProduct
    {
        public void insertData(string data)
        {
            Console.WriteLine($"insert into Product2 data = {data}");
        }

        public void updateData(string data)
        {
            Console.WriteLine($"update into Product2 data = {data}");
        }
        public void deleteData(string data)
        {
            Console.WriteLine($"delete into Product2 data = {data}");
        }
    }
}
