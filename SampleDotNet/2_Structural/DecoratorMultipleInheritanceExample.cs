using System;

namespace SampleDotNet._2_Structural
{
    internal static class DecoratorMultipleInheritanceExample
    {
        public static void Run()
        {
            var d = new Dragon();
            d.Weight = 123;
            d.Fly();
            d.Crawl();
        }

        internal interface IBird
        {
            public void Fly();
            int Weight { get; set; }
        }

        internal class Bird : IBird
        {
            public int Weight { get; set; }

            public void Fly()
            {
                Console.WriteLine($"Soaring in the sky with weight {Weight}");
            }
        }

        internal interface ILizard
        {
            public void Crawl();
            int Weight { get; set; }
        }

        internal class Lizard : ILizard
        {
            public int Weight { get; set; }

            public void Crawl()
            {
                Console.WriteLine($"Crawling in the dirt with weight {Weight}");
            }
        }

        public class Dragon : IBird, ILizard
        {
            private Bird bird = new Bird();
            private Lizard Lizard = new Lizard();
            private int _weight;

            public int Weight
            {
                get => _weight;
                set
                {
                    _weight = value;
                    bird.Weight = value;
                    Lizard.Weight = value;
                }
            }

            public void Fly()
            {
                bird.Fly();
            }

            public void Crawl()
            {
                Lizard.Crawl();
            }
        }
    }
}
