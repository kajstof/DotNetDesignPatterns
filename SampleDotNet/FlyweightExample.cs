using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace SampleDotNet
{
    [TestFixture]
    public class FlyweightExample
    {
        public static void Run()
        {
        }

        [Test]
        public void TestUser() // 1655033
        {
            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var users = new List<User>();

            foreach (var firstName in firstNames)
            foreach (var lastName in lastNames)
            {
                users.Add(new User($"{firstName} {lastName}"));
            }

            ForceGC();

            // dotMemory.Check(memory => { Console.WriteLine(memory.SizeInBytes); });
        }

        [Test]
        public void TestUser2() // 1296991
        {
            var firstNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var lastNames = Enumerable.Range(0, 100).Select(_ => RandomString());
            var users = new List<User2>();

            foreach (var firstName in firstNames)
            foreach (var lastName in lastNames)
            {
                users.Add(new User2($"{firstName} {lastName}"));
            }

            ForceGC();

            // dotMemory.Check(memory => { Console.WriteLine(memory.SizeInBytes); });
        }

        private void ForceGC()
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
            GC.Collect();
        }

        private string RandomString()
        {
            Random rand = new Random();
            return new string(Enumerable.Range(0, 10).Select(i => (char) ('a' + rand.Next(26))).ToArray());
        }

        public class User
        {
            private string fullName;

            public User(string fullName)
            {
                this.fullName = fullName;
            }
        }

        public class User2
        {
            static List<string> strings = new List<string>();
            private int[] names;

            public User2(string fullName)
            {
                int getOrAdd(string s)
                {
                    int idx = strings.IndexOf(s);
                    if (idx != -1) return idx;

                    strings.Add(s);
                    return strings.Count - 1;
                }

                names = fullName.Split(' ').Select(getOrAdd).ToArray();
            }

            public string FullName => string.Join(" ", names.Select(i => strings[i]));
        }
    }
}
