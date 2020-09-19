using System;
using System.Collections.Generic;

namespace SampleDotNet._1_Creational
{
    public static class AbstractFactoryExample
    {
        public static void Run()
        {
            var machine = new HotDrinkMachine();
            var drink = machine.MakeDrink(HotDrinkMachine.AvailableDrink.Tea, 100);
            drink.Consume();
            Console.WriteLine(drink);
        }
    }

    public interface IHotDrink
    {
        void Consume();
    }

    internal class Tea : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This tea is nice but I'd prefer it with milk.");
        }
    }

    internal class Coffee : IHotDrink
    {
        public void Consume()
        {
            Console.WriteLine("This coffee is sensational!");
        }
    }

    public interface IHotDrinkFactory
    {
        IHotDrink Prepare(int amount);
    }

    internal class TeaFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Put in a tea bag, boil water, pour {amount} ml, add lemon, enjoy!");
            return new Tea();
        }
    }

    internal class CoffeeFactory : IHotDrinkFactory
    {
        public IHotDrink Prepare(int amount)
        {
            Console.WriteLine($"Grind some beans, boil water, pour {amount} ml, add cream and sugar, enjoy!");
            return new Coffee();
        }
    }

    public class HotDrinkMachine
    {
        public enum AvailableDrink
        {
            Coffee,
            Tea
        }

        private Dictionary<AvailableDrink, IHotDrinkFactory> factories =
            new Dictionary<AvailableDrink, IHotDrinkFactory>();

        public HotDrinkMachine()
        {
            foreach (AvailableDrink drink in Enum.GetValues(typeof(AvailableDrink)))
            {
                var factory = (IHotDrinkFactory) Activator.CreateInstance(
                    Type.GetType($"{nameof(SampleDotNet)}.{nameof(HotDrinkMachine)}.{nameof(AvailableDrink)}.{Enum.GetName(typeof(AvailableDrink), drink)}"));
                factories.Add(drink, factory);
            }
        }

        public IHotDrink MakeDrink(HotDrinkMachine.AvailableDrink drink, int amount)
        {
            return factories[drink].Prepare(amount);
        }
    }
}
