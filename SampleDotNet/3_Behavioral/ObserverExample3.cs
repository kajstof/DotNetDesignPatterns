using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using JetBrains.Annotations;

namespace SampleDotNet._3_Behavioral
{
    internal static class ObserverExample3
    {
        public static void Run()
        {
            var market = new Market();
            market.PropertyChanged += (sender, args) =>
            {
                if (args.PropertyName == "Volatility")
                {
                }
            };

            var market2 = new Market2();
            // market2.PriceAdded += (sender, f) =>
            // {
            //     Console.WriteLine($"We got a price of {f}");
            // };
            market2.Prices.ListChanged += (sender, args) =>
            {
                if (args.ListChangedType == ListChangedType.ItemAdded)
                {
                    float price = ((BindingList<float>) sender)[args.NewIndex];
                    Console.WriteLine($"Binding list got a price of {price}");
                }
            };
            market2.AddPrice(123);
        }

        public class Market : INotifyPropertyChanged
        {
            private float volatility;

            public float Volatility
            {
                get => volatility;
                set
                {
                    if (value.Equals(volatility)) return;
                    volatility = value;
                    OnPropertyChanged();
                }
            }


            public event PropertyChangedEventHandler PropertyChanged;

            [NotifyPropertyChangedInvocator]
            protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
            {
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        public class Market2
        {
            // private List<float> prices = new List<float>();
            public BindingList<float> Prices = new BindingList<float>();

            public void AddPrice(float price)
            {
                Prices.Add(price);
                // PriceAdded?.Invoke(this, price);
            }

            // public EventHandler<float> PriceAdded;
        }
    }
}
