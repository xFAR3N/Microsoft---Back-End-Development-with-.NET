using System.Diagnostics;
using System.Text.Json;
using System.Xml.Serialization;

namespace Deserialization
{
    public class Program
    {
        public class Person
        {
            public string UserName { get; set; }
            public int UserAge { get; set; }
        }
        static void Main(string[] args)
        {
            Stopwatch stopwatch = Stopwatch.StartNew();

            using (var fs = new FileStream("person.dat", FileMode.Open))
            using (var reader = new BinaryReader(fs))
            {
                var deserializedPerson = new Person
                {
                    UserName = reader.ReadString(),
                    UserAge = reader.ReadInt32(),
                };

                stopwatch.Stop();
                Console.WriteLine($"Binanry Deserialization:\nUserName: {deserializedPerson.UserName}, \nUserAge: {deserializedPerson.UserAge}");
                Console.WriteLine($"Binary Deserialization took: {stopwatch.ElapsedMilliseconds} miliseconds");
            }

            var xmlData = "<Person><UserName>Alice</UserName><UserAge>30</UserAge></Person>";
            var serializer = new XmlSerializer(typeof(Person));

            stopwatch.Restart();
            stopwatch.Start();

            using (var reader = new StringReader(xmlData))
            {
                var deserializedPersonXml = (Person)serializer.Deserialize(reader);

                stopwatch.Stop();
                Console.WriteLine($"XML Deserialization:\nUserName: {deserializedPersonXml.UserName},\nUserAge: {deserializedPersonXml.UserAge}");
                Console.WriteLine($"XML Deserialization took: {stopwatch.ElapsedMilliseconds} miliseconds");
            }

            stopwatch.Restart();

            var jsonData = "{\"UserName\": \"Charlie\", \"UserAge\": 25}";

            stopwatch.Start();

            var deserializedPersonJson = JsonSerializer.Deserialize<Person>(jsonData);

            stopwatch.Stop();

            Console.WriteLine($"XML Deserialization:\nUserName: {deserializedPersonJson.UserName},\nUserAge: {deserializedPersonJson.UserAge}");
            Console.WriteLine($"XML Deserialization took: {stopwatch.ElapsedMilliseconds} miliseconds");
        }
    }
}
