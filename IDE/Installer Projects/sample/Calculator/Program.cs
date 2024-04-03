using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Calculate;

namespace Calculator
{
    class Program
    {
        static void Main(string[] args)
        {
            double result = CalculateService.Add(1, 2);
            Console.WriteLine($"Add(1, 2) = {result}\n");

            result = CalculateService.Subtract(1, 2);
            Console.WriteLine($"Subtract(1, 2) = {result}\n");

            result = CalculateService.Multiply(1, 2);
            Console.WriteLine($"Multiply(1, 2) = {result}\n");

            result = CalculateService.Divide(1, 2);
            Console.WriteLine($"Divide(1, 2) = {result}\n");
        }
    }
}
