using System;

namespace SampleDotNet._2_Structural
{
    internal static class ProtectionProxyExample
    {
        public static void Run()
        {
            ICar car = new CarProxy(new Driver(22));
            car.Drive();
        }

        public interface ICar
        {
            void Drive();
        }

        public class Car : ICar
        {
            public void Drive()
            {
                Console.WriteLine("Car is being driven");
            }
        }

        public class Driver
        {
            public int Age { get; set; }

            public Driver(int age)
            {
                Age = age;
            }
        }

        public class CarProxy : ICar
        {
            private readonly Driver _driver;
            private Car car = new Car();

            public CarProxy(Driver driver)
            {
                _driver = driver;
            }

            public void Drive()
            {
                if (_driver.Age >= 16)
                    car.Drive();
                else
                {
                    Console.WriteLine("Too young");
                }
            }
        }
    }
}
