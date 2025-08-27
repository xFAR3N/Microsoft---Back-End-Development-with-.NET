using Newtonsoft.Json;
namespace Microsoft___Back_End_Development_with_.NET
{
    public class Product
    {
        public string Name { get; set; }
        public float Price { get; set; }
        public List<string> Tags { get; set; }
    }
    public class Program
    {
        static void Main(string[] args)
        {
            //Deserialization

            string json = "{\"Name\": \"Laptop\", \"Price\": 999.99, \"Tags\": [\"Electronics\", \"Computers\"]}";
            Product laptop = JsonConvert.DeserializeObject<Product>(json);
            Console.WriteLine($"Deserialized object: \n Product: {laptop.Name}, Price: {laptop.Price}, Tags: {string.Join(", ", laptop.Tags)}");

            //Serialization

            Product smartphone = new Product
            {
                Name = "Smartphone",
                Price = 799.99f,
                Tags = new List<string> { "Electronics", "Mobile" },
            };

            string smartphoneJson = JsonConvert.SerializeObject(smartphone, Formatting.Indented);

            Console.WriteLine($"Serialized object: \n {smartphoneJson}");
        }
    }
}
