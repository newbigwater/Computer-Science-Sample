using System;
using System.Collections.Generic;

namespace Flyweight
{
    public class FlyweightFactory
    {
        private Dictionary<string, Flyweight> flyweights = new Dictionary<string, Flyweight>();

        public Flyweight GetFlyweight(string key)
        {
            if (!flyweights.ContainsKey(key))
            {
                flyweights[key] = new ConcreteFlyweight(key);
                Console.WriteLine($"FlyweightFactory: Created new flyweight for key {key}");
            }
            else
            {
                Console.WriteLine($"FlyweightFactory: Reusing existing flyweight for key {key}");
            }
            return flyweights[key];
        }
    }
}
