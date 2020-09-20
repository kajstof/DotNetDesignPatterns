using System;
using System.Collections.Generic;
using Dynamitey.DynamicObjects;

namespace SampleDotNet._3_Behavioral
{
    internal abstract class StateHandmadeExample
    {
        private static Dictionary<State, List<(Trigger, State)>> _rules =
            new Dictionary<State, List<(Trigger, State)>>()
            {
                [State.OffHook] = new List<(Trigger, State)>
                {
                    (Trigger.CallDialed, State.Connecting)
                },
                [State.Connecting] = new List<(Trigger, State)>
                {
                    (Trigger.HangUp, State.OffHook),
                    (Trigger.CallConnected, State.Connected),
                },
                [State.Connected] = new List<(Trigger, State)>
                {
                    (Trigger.LeftMessage, State.OffHook),
                    (Trigger.HangUp, State.OffHook),
                    (Trigger.PlacedOnHold, State.OnHold),
                },
                [State.OnHold] = new List<(Trigger, State)>
                {
                    (Trigger.TakenOffHold, State.Connected),
                    (Trigger.HangUp, State.OffHook)
                }
            };

        public static void Run()
        {
            var state = State.OffHook;
            while (true)
            {
                Console.WriteLine($"The phone is currently {state}");
                Console.WriteLine("Select a trigger:");
                for (var i = 0; i < _rules[state].Count; i++)
                {
                    var (t, _) = _rules[state][i];
                    Console.WriteLine($"{i}. {t}");
                }

                int input = int.Parse(Console.ReadLine());
                var (_, s) = _rules[state][input];
                state = s;
            }
        }

        public enum State
        {
            OffHook,
            Connecting,
            Connected,
            OnHold
        }

        public enum Trigger
        {
            CallDialed,
            HangUp,
            CallConnected,
            PlacedOnHold,
            TakenOffHold,
            LeftMessage
        }
    }
}
