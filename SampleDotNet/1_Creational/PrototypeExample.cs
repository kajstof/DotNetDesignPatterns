using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace SampleDotNet._1_Creational
{
    public static class PrototypeExample
    {
        public static void Run()
        {
            var john = new Person(new[] {"John", "Smith"}, new Address("London Road", 123));

            // var jane = new Person(john);
            var jane = john.DeepCopyXml();
            jane.Names[0] = "Jane";
            jane.Address.HouseNumber = 231;

            Console.WriteLine(john);
            Console.WriteLine(jane);
        }


        // private interface IPrototype<T>
        // {
        //     T DeepCopy();
        // }

        // [Serializable]
        public class Person
        {
            public string[] Names;
            public Address Address;

            public Person()
            {
            }

            public Person(string[] names, Address address)
            {
                Names = names ?? throw new ArgumentNullException(nameof(names));
                Address = address ?? throw new ArgumentNullException(nameof(address));
            }

            public Person(Person other)
            {
                Names = other.Names;
                Address = new Address(other.Address);
            }

            // public Person DeepCopy()
            // {
            //     return new Person(Names, Address.DeepCopy());
            // }

            public override string ToString()
            {
                return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
            }
        }

        // [Serializable]
        public class Address
        {
            public string StreetName { get; set; }
            public int HouseNumber { get; set; }

            public Address()
            {
            }

            public Address(string streetName, int houseNumber)
            {
                StreetName = streetName ?? throw new ArgumentNullException(nameof(streetName));
                HouseNumber = houseNumber;
            }

            public Address(Address other)
            {
                StreetName = other.StreetName;
                HouseNumber = other.HouseNumber;
            }

            // public Address DeepCopy()
            // {
            //     return new Address(StreetName, HouseNumber);
            // }

            public override string ToString()
            {
                return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
            }
        }
    }

    public static partial class ExtensionMethods
    {
        public static T DeepCopy<T>(this T self)
        {
            var stream = new MemoryStream();
            var formatter = new BinaryFormatter();
            formatter.Serialize(stream, self);
            stream.Seek(0, SeekOrigin.Begin);
            object copy = formatter.Deserialize(stream);
            stream.Close();
            return (T) copy;
        }

        public static T DeepCopyXml<T>(this T self)
        {
            using (var ms = new MemoryStream())
            {
                var s = new XmlSerializer(typeof(T));
                s.Serialize(ms, self);
                ms.Position = 0;
                return (T) s.Deserialize(ms);
            }
        }
    }
}
