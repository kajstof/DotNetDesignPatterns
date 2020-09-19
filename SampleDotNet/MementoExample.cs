﻿using System;
using System.Collections.Generic;

namespace SampleDotNet
{
    internal static class MementoExample
    {
        public static void Run()
        {
            var ba = new BankAccount(100);
            var m1 = ba.Deposit(50); // 150
            var m2 = ba.Deposit(25); // 175
            Console.WriteLine(ba);

            ba.Restore(m1);
            Console.WriteLine(ba);

            ba.Restore(m2);
            Console.WriteLine(ba);

            var ba2 = new BankAccount(100);
            ba2.Deposit(50);
            ba2.Deposit(25);
            Console.WriteLine(ba2);

            ba2.Undo();
            Console.WriteLine($"Undo 1: {ba2}");
            ba2.Undo();
            Console.WriteLine($"Undo 2: {ba2}");
            ba2.Redo();
            Console.WriteLine($"Redo 1: {ba2}");
        }

        public class Memento
        {
            public int Balance { get; }

            public Memento(int balance)
            {
                Balance = balance;
            }
        }

        public class BankAccount
        {
            private int balance;
            private List<Memento> changes = new List<Memento>();
            private int current;

            public BankAccount(int balance)
            {
                this.balance = balance;
                changes.Add(new Memento(balance));
            }

            public Memento Deposit(int amount)
            {
                balance += amount;
                var m = new Memento(balance);
                changes.Add(m);
                ++current;
                return m;
            }

            public Memento Restore(Memento m)
            {
                if (m != null)
                {
                    balance = m.Balance;
                    changes.Add(m);
                    return m;
                }

                return null;
            }

            public Memento Undo()
            {
                if (current > 0)
                {
                    var m = changes[--current];
                    balance = m.Balance;
                    return m;
                }

                return null;
            }

            public Memento Redo()
            {
                if (current + 1 < changes.Count)
                {
                    var m = changes[++current];
                    balance = m.Balance;
                    return m;
                }

                return null;
            }

            public override string ToString()
            {
                return $"{nameof(balance)}: {balance}";
            }
        }
    }
}
