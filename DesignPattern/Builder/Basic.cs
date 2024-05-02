using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    public interface IBuilder
    {
        void BuildPartA();
        void BuildPartB();
        void BuildPartC();
        Product GetResult();
    }

    public class Product
    {
        private string partA;
        private string partB;
        private string partC;

        public Product(string partA, string partB, string partC)
        {
            this.partA = partA;
            this.partB = partB;
            this.partC = partC;
        }

        public void Display()
        {
            Console.WriteLine($"Part A: {partA}, Part B: {partB}, Part C: {partC}");
        }
    }

    public class ConcreteBuilder : IBuilder
    {
        private string partA;
        private string partB;
        private string partC;

        public void BuildPartA()
        {
            partA = "Part A";
        }

        public void BuildPartB()
        {
            partB = "Part B";
        }

        public void BuildPartC()
        {
            partC = "Part C";
        }

        public Product GetResult()
        {
            return new Product(partA, partB, partC);
        }
    }

    public class Diractor
    {
        public IBuilder builder;

        public Diractor(IBuilder builder)
        {
            this.builder = builder;
        }

        public IBuilder Construct()
        {
            builder.BuildPartA();
            builder.BuildPartB();
            builder.BuildPartC();

            return builder;
        }
    }
}
