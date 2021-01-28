using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIPreventivas.Models;
using APIPreventivas.Service;

namespace APIPreventivas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlvosController : ControllerBase
    {
        private readonly APIPreventivaContext _context;

        public AlvosController(APIPreventivaContext context)
        {
            _context = context;
        }

        // GET: api/Alvos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Alvo>>> GetAlvos()
        {
            return await _context.Alvos.ToListAsync();
        }

        // GET: api/Alvos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Alvo>> GetAlvo(int id)
        {
            var alvo = await _context.Alvos.FindAsync(id);

            if (alvo == null)
            {
                return NotFound();
            }

            return alvo;
        }

        // GET: api/Alvos/MGPSO_0001
        [HttpGet("{endId}")]
        public async Task<ActionResult<Alvo>> GetAlvoEndId(string EndId)
        {
            var alvo = await _context.Alvos.FindAsync(EndId);

            if (alvo == null)
            {
                return NotFound();
            }

            return alvo;
        }

        // PUT: api/Alvos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAlvo(int id, Alvo alvo)
        {
            if (id != alvo.IdAlvo)
            {
                return BadRequest();
            }

            _context.Entry(alvo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                AlvoService.AlteraStatusAlvo(alvo);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlvoExists(id))
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

        // POST: api/Alvos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Alvo>> PostAlvo(Alvo alvo)
        {
            _context.Alvos.Add(alvo);
            await _context.SaveChangesAsync();
            AlvoService.relacionaAlvoSite(alvo);

            return CreatedAtAction("GetAlvo", new { id = alvo.IdAlvo }, alvo);
        }

        // DELETE: api/Alvos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Alvo>> DeleteAlvo(int id)
        {
            var alvo = await _context.Alvos.FindAsync(id);
            if (alvo == null)
            {
                return NotFound();
            }

            _context.Alvos.Remove(alvo);
            await _context.SaveChangesAsync();

            return alvo;
        }

        private bool AlvoExists(int id)
        {
            return _context.Alvos.Any(e => e.IdAlvo == id);
        }
    }
}