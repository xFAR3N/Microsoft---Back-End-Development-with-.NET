using System.IO;
using System.Text.Json;
using System.Xml.Serialization;

namespace Serialization
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
            Person samplePerson = new Person { UserName = "Alice", UserAge = 30 };

            using (FileStream fs = new FileStream("person.dat", FileMode.Create))
            {
                BinaryWriter writer = new BinaryWriter(fs);
                writer.Write(samplePerson.UserName);
                writer.Write(samplePerson.UserAge);
            }
            Console.WriteLine("Serialization complete.");

            XmlSerializer xmlSerializer = new XmlSerializer(typeof(Person));

            using (StreamWriter writer = new StreamWriter("person.xml"))
            {
                xmlSerializer.Serialize(writer, samplePerson);
            }

            string jsonString = JsonSerializer.Serialize(samplePerson);

            File.WriteAllText("person.json", jsonString);

            Console.WriteLine("JSON serialization complete");
        }
    }
}
