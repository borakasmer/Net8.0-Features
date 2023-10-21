using System;
using System.Linq;
using System.Collections.Generic;
using GoogleMapPoint = (string, double);
using System.Security.Cryptography.X509Certificates;
using System.Reflection.Metadata.Ecma335;
using System.Text.Json;

namespace Net8._0_Features
{
    public class Program
    {       
        public static void Main(string[] args)
        {
            var i = 5;
            System.Console.WriteLine(new Car().Name(ref i)); // prints "E" in C# 11, but "C" in C# 12
            System.Console.WriteLine(CarExtension.Name(new Car(), ref i));
            System.Console.WriteLine("".PadRight(80, '*'));
            IGeo circile = new Circle { Pi = 3.141592, R = 2 };
            var jsonCircile = JsonSerializer.Serialize(circile);
            Console.WriteLine("Circile Json: " + jsonCircile);

            IGeo square = new Square { X = 5 };
            var options = new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.KebabCaseUpper };         
            var jsonSquare = JsonSerializer.Serialize(square, options);
            Console.WriteLine("Square Json: " + jsonSquare);
            options.MakeReadOnly();

            List<Person> persons = new List<Person>();
            persons.AddRange(new Person[] {
                new Person("John", "Doe", 20,("Newyork",123)),
                new Person("Jane", "Saw", 30,("LA",456)),
                new Person("Tarzan", "Jungle", 40,("Seattle",666)),
                new Person("Bora", "Kasmer", 45,("IST",412)),
                new Person("Martin", "Fowler", 50,("MC",734))
            });

            //Lambda expression parameter's default values
            var defaultVergiOrani = (int vergi = 18) => vergi + 2;
            Console.WriteLine("Default Vergi Orani: " + defaultVergiOrani());
            Console.WriteLine("Default Vergi Orani: " + defaultVergiOrani(28));

            /*persons.Add(new Person() { Name = "John", Surname = "Doe", Age = 20 });
            persons.Add(new Person() { Name = "Jane", Surname = "Saw", Age = 30 });
            persons.Add(new Person() { Name = "Tarzan", Surname = "Jungle", Age = 40 });
            persons.Add(new Person() { Name = "Bora", Surname = "Kasmer", Age = 45 });
            persons.Add(new Person() { Name = "Martin", Surname = "Fowler", Age = 50 });*/

            Console.WriteLine("".PadRight(60, '*'));
            Console.WriteLine("Random Persons:");
            //var randomPersons = Random.Shared.GetItems(persons.ToArray(), 3);
            var randomPersons = GetRandomItemFormList(persons, 3);
            randomPersons.ForEach(p => Console.WriteLine($"{p.Name}-{p.Surname}-{p.Age}-{p.Point}"));
            Console.WriteLine("".PadRight(60, '*'));

            Console.WriteLine("Shuffle Numbers:");
            int[] numbers = [1, 2, 3, 4, 5, 6, 7, 8, 9, 10];
            Random.Shared.Shuffle(numbers);
            int index = 1;
            numbers.ToList().ForEach(n => Console.WriteLine($"{index++}. number => {n}"));

            /*public static Person[] persons = new[]{new Person()
            {Name = "John", Surname = "Doe", Age = 20}, new Person()
            {Name = "Jane", Surname = "Saw", Age = 30}, new Person()
            {Name = "Tarzan", Surname = "Jungle", Age = 40}, new Person()
            {Name = "Bora", Surname = "Kasmer", Age = 50}, new Person()
            {Name = "Martin", Surname = "Fowler", Age = 65}};*/
        }

        public static List<Person> GetRandomItemFormList(List<Person> persons, int count)
        {
            var randomPersons = Random.Shared.GetItems(persons.ToArray(), count).Distinct();
            //var personFinalList = randomPersons.Count() < count ? Random.Shared.GetItems(persons.Except(randomPersons).ToArray(), count- randomPersons.Count()) : randomPersons;
            var personFinalList2 = randomPersons.Count() < count ? persons.Except(randomPersons).Take(count - randomPersons.Count()) : randomPersons;
            var returnList = randomPersons.ToList();
            //if(randomPersons.Count() < count) returnList.AddRange(personFinalList);
            if (randomPersons.Count() < count) returnList.AddRange(personFinalList2);
            return returnList;
        }
    }

    public class Person(string name, string surname, int age, GoogleMapPoint point)
    {
        public string Name => name;
        public string Surname => surname;
        public int Age => age;
        public GoogleMapPoint Point => point;
    }
    /*public class Person
    {
        public string Name { get; set; }

        public string Surname { get; set; }

        public int Age { get; set; }
    }*/

    public interface IBaseMath
    {
        public double? Pi { get; set; }
        public string Type { get; set; }
    }
    public interface IGeo : IBaseMath
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int? H { get; set; }
        public double? R { get; set; }
        public double? FindArea();
        public double? FindPerimeter();
    }
    public class Square : IGeo
    {
        int x, y;
        int? h;
        double? r;
        string type;
        public double? Pi { get; set; }
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int? H { get => h; set => h = value; }
        public double? R { get => r; set => r = value; }
        public string Type { get => type; set => type = value; }

        public double? FindArea()
        {
            return Math.Pow(X, 2);
        }

        public double? FindPerimeter()
        {
            return 4 * X;
        }
    }
    public class Circle : IGeo
    {
        double? pi;
        public double? Pi { get { return pi; } set { pi = value; } }

        int x, y;
        int? h;
        double? r;
        string type;
        public int X { get => x; set => x = value; }
        public int Y { get => y; set => y = value; }
        public int? H { get => h; set => h = value; }
        public double? R { get => r; set => r = value; }
        public string Type { get => type; set => type = value; }

        public double? FindArea(int x, int? h, double? r)
        {
            return pi * Math.Pow((double)r, 2);
        }

        public double? FindArea()
        {
            throw new NotImplementedException();
        }

        public double? FindPerimeter()
        {
            return 2 * Pi * R;
        }
    }

    class Car
    {
        public string Name(in int i) => "Car";
    }
    static class CarExtension
    {
        public static string Name(this Car c, ref int i) => "Park";
    }
}