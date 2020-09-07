using System;

namespace SampleDotNet
{
    public class Person2
    {
        // address
        public string StreetAddress, Postcode, City;

        // employment
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return
                $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}, {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}, {nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
        }
    }

    public class PersonBuilder // facade
    {
        // reference!
        protected Person2 person = new Person2();

        public PersonJobBuilder Works => new PersonJobBuilder(person);
        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);

        public Person2 Build => person;

        public static implicit operator Person2(PersonBuilder pb)
        {
            return pb.person;
        }
    }

    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person2 person)
        {
            this.person = person;
        }

        public PersonAddressBuilder At(string streetAddress)
        {
            person.StreetAddress = streetAddress;
            return this;
        }

        public PersonAddressBuilder WithPostcode(string postcode)
        {
            person.Postcode = postcode;
            return this;
        }

        public PersonAddressBuilder In(string city)
        {
            person.City = city;
            return this;
        }
    }

    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person2 person)
        {
            this.person = person;
        }

        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int amount)
        {
            person.AnnualIncome = amount;
            return this;
        }
    }

    public static class FacetedBuilderExample
    {
        public static void Run()
        {
            var pb = new PersonBuilder();
            Person2 person = pb.Works.At("Fabrikam")
                                 .AsA("Engineer")
                                 .Earning(123000)
                .Lives.At("SomeStreet 177")
                      .WithPostcode("00-123")
                      .In("New York");
            Console.WriteLine(person);
        }
    }
}
