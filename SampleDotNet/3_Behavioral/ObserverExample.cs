using System;

namespace SampleDotNet._3_Behavioral
{
    internal static class ObserverExample
    {
        public static void Run()
        {
            var person = new Person();
            person.FallsIll += CallDoctor;
            person.CatchACold();
            person.FallsIll -= CallDoctor;
        }

        private static void CallDoctor(object sender, FallsIllEventArgs e)
        {
            Console.WriteLine($"A doctor has been called to {e.Address}");
        }

        public class Person
        {
            public void CatchACold()
            {
                FallsIll?.Invoke(this, new FallsIllEventArgs {Address = "123 London Road"});
            }

            public event EventHandler<FallsIllEventArgs> FallsIll;
        }

        public class FallsIllEventArgs : EventArgs
        {
            public string Address;
        }
    }
}
