using System;
using System.Collections.Generic;

namespace FactoryMethod
{
    public abstract class Car
    {
        public string carType;

        public String toString()
        {
            return carType;
        }
    }

    public interface ICarFactory
    {
        public Car CreateCar(string name);
        public Car ReturnMyCar(string name);
    }

    public class HyundaiFactory : ICarFactory
    {
        Dictionary<string, Car> carDic = new Dictionary<string, Car>();

        public Car CreateCar(string name)
        {
            Car car = null;

            if (name == "sonata") { car = new Sonata(); }
            else if (name == "santafe") { car = new Santafe(); }

            return car;
        }

        public Car ReturnMyCar(string name)
        {
            // Jame는 Sonata, Tomas는 Santafe 인 경우
            Car myCar = null;
            if (carDic.ContainsKey(name))
            {
                myCar = carDic[name];
            }

            if (myCar == null)
            {
                if (name.Equals("James")) { myCar = new Sonata(); }
                else if (name.Equals("Tomas")) { myCar = new Santafe(); }
                carDic[name] = myCar;
            }

            return myCar;
        }
    }

    public class Sonata : Car
    {
        public Sonata()
        {
            carType = "Sonata";
        }
    }

    public class Santafe : Car
    {
        public Santafe()
        {
            carType = "Santafe";
        }
    }
}
