using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Builder
{
    public abstract class Pizza
    {
        public enum Topping { HAM, MUSHROOM, ONION, PEPPER, SAUSAGE };
        protected readonly HashSet<Topping> toppings;

        public abstract class Builder
        {
            public HashSet<Topping> toppings = new HashSet<Topping>();

            public Builder AddTopping(Topping topping)
            {
                toppings.Add(topping);

                return Self();
            }
            public virtual Builder SauceInside() { return Self(); }

            public abstract Pizza Build();

            protected abstract Builder Self();
        }

        protected Pizza(Builder builder)
        {
            toppings = new HashSet<Topping>(builder.toppings);
        }

        public virtual string ToStringToppings()
        {
            return string.Join(", ", toppings);
        }
    }

    public class NyPizza : Pizza
    {
        public enum Size { SMALL, MEDIUM, LARGE };
        private Size size;

        public class Builder : Pizza.Builder
        {
            public Size size;

            public Builder(Size size) { this.size = size; }

            public override Pizza Build() { return new NyPizza(this); }

            protected override Pizza.Builder Self() { return this; }
        }

        private NyPizza(Builder builder) : base(builder)
        {
            size = builder.size;
        }
    }

    public class Calzone : Pizza
    {
        private bool sauceInside;

        public class Builder : Pizza.Builder
        {
            public bool sauceInside = false;

            public override Pizza.Builder SauceInside()
            {
                sauceInside = true;
                return (Calzone.Builder)Self();
            }
            public override Pizza Build() { return new Calzone(this); }

            protected override Pizza.Builder Self() { return this; }
        }

        private Calzone(Builder builder) : base(builder)
        {
            sauceInside = builder.sauceInside;
        }

        public override String ToStringToppings()
        {
            return string.Join(", ", toppings) + " sauceInside: " + sauceInside;
        }
    }
}
