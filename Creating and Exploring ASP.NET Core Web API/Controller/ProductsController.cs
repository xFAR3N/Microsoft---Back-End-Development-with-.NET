using Creating_and_Exploring_ASP.NET_Core_Web_API.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Creating_and_Exploring_ASP.NET_Core_Web_API.Controller
{
    [Route("api/products")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        private static List<Product> products = new List<Product>();
        [HttpGet]
        public ActionResult<List<Product>> GetAll() => products;

        [HttpGet("{id}")]
        public ActionResult<Product> GetById([FromRoute]int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            return product != null ? Ok(product) : NotFound();
        }

        [HttpPost]
        public ActionResult CreateProduct([FromBody] Product newProduct)
        {
            newProduct.Id = products.Count + 1;
            products.Add(newProduct);
            return CreatedAtAction(nameof(GetById), new { id = newProduct.Id }, newProduct);
        }

        [HttpPut("{id}")]
        public ActionResult Update(int id, Product updatedProduct)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if(product == null)
            {
                return NotFound("Product not found");
            }

            product.Name = updatedProduct.Name;
            product.Price = updatedProduct.Price;
            product.Description = updatedProduct.Description;

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            var product = products.FirstOrDefault(p => p.Id == id);
            if(product == null)
            {
                return NotFound("Product not found");
            }

            products.Remove(product);

            return NoContent();
        }

    }
}
