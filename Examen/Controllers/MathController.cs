using Microsoft.AspNetCore.Mvc;

namespace Examen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MathController : ControllerBase
    {
        [HttpGet("operation")]
        public IActionResult GetOperation([FromHeader] double num1, [FromHeader] double num2, [FromHeader] string op)
        {
            double result = op switch
            {
                "sum" => num1 + num2,
                "res" => num1 - num2,
                "mul" => num1 * num2,
                "div" => num1 / num2,
                _ => throw new InvalidOperationException("Invalid operation")
            };
            return Ok(result);
        }

        [HttpPost("operation")]
        public IActionResult PostOperation([FromBody] MathOperation operation)
        {
            double result = operation.Operacion switch
            {
                "sum" => operation.Num1 + operation.Num2,
                "res" => operation.Num1 - operation.Num2,
                "mul" => operation.Num1 * operation.Num2,
                "div" => operation.Num1 / operation.Num2,
                _ => throw new InvalidOperationException("Invalid operation")
            };
            return Ok(result);
        }
    }

    public class MathOperation
    {
        public double Num1 { get; set; }
        public double Num2 { get; set; }
        public string Operacion { get; set; }
    }
}
