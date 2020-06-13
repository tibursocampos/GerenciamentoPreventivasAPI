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
    public class TecnicosAlvosController : ControllerBase
    {
        private readonly APIPreventivaContext _context;

        public TecnicosAlvosController(APIPreventivaContext context)
        {
            _context = context;
        }

        // GET: api/TecnicosAlvos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<TecnicoAlvo>>> GetTecnicosAlvos()
        {
            return await _context.TecnicosAlvos.ToListAsync();
        }

        // GET: api/TecnicosAlvos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<TecnicoAlvo>> GetTecnicoAlvo(int id)
        {
            var tecnicoAlvo = await _context.TecnicosAlvos.FindAsync(id);

            if (tecnicoAlvo == null)
            {
                return NotFound();
            }

            return tecnicoAlvo;
        }

        // PUT: api/TecnicosAlvos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTecnicoAlvo(int id, TecnicoAlvo tecnicoAlvo)
        {
            if (id != tecnicoAlvo.Id_alvo)
            {
                return BadRequest();
            }

            _context.Entry(tecnicoAlvo).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TecnicoAlvoExists(id))
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

        // POST: api/TecnicosAlvos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<TecnicoAlvo>> PostTecnicoAlvo(TecnicoAlvo tecnicoAlvo)
        {
            _context.TecnicosAlvos.Add(tecnicoAlvo);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (TecnicoAlvoExists(tecnicoAlvo.Id_alvo))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetTecnicoAlvo", new { id = tecnicoAlvo.Id_alvo }, tecnicoAlvo);
        }

        // DELETE: api/TecnicosAlvos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<TecnicoAlvo>> DeleteTecnicoAlvo(int id)
        {
            var tecnicoAlvo = await _context.TecnicosAlvos.FindAsync(id);
            if (tecnicoAlvo == null)
            {
                return NotFound();
            }

            _context.TecnicosAlvos.Remove(tecnicoAlvo);
            await _context.SaveChangesAsync();

            return tecnicoAlvo;
        }

        private bool TecnicoAlvoExists(int id)
        {
            return _context.TecnicosAlvos.Any(e => e.Id_alvo == id);
        }
    }
}
