using System;

namespace SampleDotNet._1_Creational
{
    class Person
    {
        public string Name { get; set; }
        public string Position { get; set; }

        public class Builder : PersonJobBuilder<Builder>
        {
        }

        public static Builder New => new Builder();

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }

        public abstract class PersonBuilder
        {
            protected Person person = new Person();

            public Person Build()
            {
                return person;
            }
        }

        public class PersonInfoBuilder<TSelf> : PersonBuilder
            where TSelf : PersonInfoBuilder<TSelf>
        {
            public TSelf Called(string name)
            {
                person.Name = name;
                return (TSelf) this;
            }
        }

        public class PersonJobBuilder<TSelf> : PersonInfoBuilder<PersonJobBuilder<TSelf>>
            where TSelf : PersonJobBuilder<TSelf>
        {
            public TSelf WorksAsA(string position)
            {
                person.Position = position;
                return (TSelf) this;
            }
        }
    }

    public static class BuilderAbstractExample
    {
        public static void Run()
        {
            // var builder = new Person.PersonJobBuilder();
            // builder.Called("Krzyś").WorksAsA
            // Console.WriteLine("Hello World!");
            var person = Person.New
                .Called("Krzyś")
                .WorksAsA("Quant")
                .Build();

            Console.WriteLine(person);
        }
    }
}
