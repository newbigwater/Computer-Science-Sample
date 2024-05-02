using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    class Program
    {
        static void Main(string[] args)
        {
            IBuilder builder = new ConcreteBuilder();
            Diractor diractor = new Diractor(builder);
            diractor.Construct();
            Product product = builder.GetResult();
            product.Display();

            new Diractor(new ConcreteBuilder()).Construct().GetResult().Display();


            Pizza nyPizza = new NyPizza.Builder(NyPizza.Size.SMALL)
                            .AddTopping(Pizza.Topping.SAUSAGE)
                            .AddTopping(Pizza.Topping.ONION)
                            .Build();
            Pizza calzone = new Calzone.Builder()
                            .AddTopping(Pizza.Topping.HAM)
                            .AddTopping(Pizza.Topping.PEPPER)
                            .SauceInside()
                            .Build();

            Console.WriteLine(nyPizza.ToStringToppings());
            Console.WriteLine(calzone.ToStringToppings());

            Console.ReadLine();
        }
    }
}
