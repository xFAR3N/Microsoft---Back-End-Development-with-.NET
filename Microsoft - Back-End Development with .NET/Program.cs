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
            string json = "{\"Name\": \"Laptop\", \"Price\": 999.99, \"Tags\": [\"Electronics\", \"Computers\"]}";
            Product product = JsonConvert.DeserializeObject<Product>(json);
            Console.WriteLine($"Product: {product.Name}, Price: {product.Price}, Tags: {string.Join(", ", product.Tags)}");
        }
    }
}
