using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIPreventivas.Models;

namespace APIPreventivas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CronogramasController : ControllerBase
    {
        private readonly APIPreventivaContext _context;

        public CronogramasController(APIPreventivaContext context)
        {
            _context = context;
        }

        // GET: api/Cronogramas
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Cronograma>>> GetCronogramas()
        {
            return await _context.Cronogramas.ToListAsync();
        }

        // GET: api/Cronogramas/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Cronograma>> GetCronograma(int id)
        {
            var cronograma = await _context.Cronogramas.FindAsync(id);

            if (cronograma == null)
            {
                return NotFound();
            }

            return cronograma;
        }

        // PUT: api/Cronogramas/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCronograma(int id, Cronograma cronograma)
        {
            if (id != cronograma.Id_cronograma)
            {
                return BadRequest();
            }

            _context.Entry(cronograma).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!CronogramaExists(id))
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

        // POST: api/Cronogramas
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Cronograma>> PostCronograma(Cronograma cronograma)
        {
            _context.Cronogramas.Add(cronograma);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCronograma", new { id = cronograma.Id_cronograma }, cronograma);
        }

        // DELETE: api/Cronogramas/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Cronograma>> DeleteCronograma(int id)
        {
            var cronograma = await _context.Cronogramas.FindAsync(id);
            if (cronograma == null)
            {
                return NotFound();
            }

            _context.Cronogramas.Remove(cronograma);
            await _context.SaveChangesAsync();

            return cronograma;
        }

        private bool CronogramaExists(int id)
        {
            return _context.Cronogramas.Any(e => e.Id_cronograma == id);
        }
    }
}
