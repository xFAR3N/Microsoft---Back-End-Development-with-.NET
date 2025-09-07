using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ErrorHandlingAndLogging.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ErrorHandlingController : ControllerBase
    {
        [HttpGet("division")]
        public IActionResult GetDivisionResult(int numerator, int denumerator)
        {
            try
            {
                var result = numerator / denumerator;
                return Ok($"Here's the result: {result}");
            }
            catch(DivideByZeroException)
            {
                Console.WriteLine("Error: Division by zero is not allowed");
                return BadRequest("Cannot divide by zero.");
            }
        }
    }
}
