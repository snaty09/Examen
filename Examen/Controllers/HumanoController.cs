using Examen.Data;
using Examen.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Examen.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HumanoController : ControllerBase
    {
        private readonly HumanoContext _context;

        public HumanoController(HumanoContext context)
        {
            _context = context;
            // Agregar datos mock si la tabla está vacía
            if (!_context.Humanos.Any())
            {
                _context.Humanos.AddRange(
                    new Humano { Id = 1,Nombre = "John Doe", Sexo = "M", Edad = 30, Altura = 1.75f, Peso = 70 },
                    new Humano { Id = 2,Nombre = "Jane Doe", Sexo = "F", Edad = 25, Altura = 1.65f, Peso = 60 }
                );
                _context.SaveChanges();
            }
        }
   

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Humano>>> GetHumanos()
        {
            return await _context.Humanos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Humano>> GetHumano(int id)
        {
            var humano = await _context.Humanos.FindAsync(id);

            if (humano == null)
            {
                return NotFound();
            }

            return humano;
        }


        [HttpPost]
        public async Task<ActionResult<Humano>> PostHumano(Humano humano)
        {
            _context.Humanos.Add(humano);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetHumano), new { id = humano.Id }, humano);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutHumano(int id, Humano humano)
        {
            if (id != humano.Id)
            {
                return BadRequest();
            }

            _context.Entry(humano).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Humanos.Any(e => e.Id == id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }
    }
}
