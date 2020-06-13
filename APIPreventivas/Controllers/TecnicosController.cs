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
    public class TecnicosController : ControllerBase
    {
        private readonly APIPreventivaContext _context;

        public TecnicosController(APIPreventivaContext context)
        {
            _context = context;
        }

        // GET: api/Tecnicos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tecnico>>> GetTecnicos()
        {
            return await _context.Tecnicos.ToListAsync();
        }

        // GET: api/Tecnicos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Tecnico>> GetTecnico(int id)
        {
            var tecnico = await _context.Tecnicos.FindAsync(id);

            if (tecnico == null)
            {
                return NotFound();
            }

            return tecnico;
        }

        // PUT: api/Tecnicos/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutTecnico(int id, Tecnico tecnico)
        {
            if (id != tecnico.Id_funcionario)
            {
                return BadRequest();
            }

            _context.Entry(tecnico).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TecnicoExists(id))
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

        // POST: api/Tecnicos
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Tecnico>> PostTecnico(Tecnico tecnico)
        {
            _context.Tecnicos.Add(tecnico);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetTecnico", new { id = tecnico.Id_funcionario }, tecnico);
        }

        // DELETE: api/Tecnicos/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Tecnico>> DeleteTecnico(int id)
        {
            var tecnico = await _context.Tecnicos.FindAsync(id);
            if (tecnico == null)
            {
                return NotFound();
            }

            _context.Tecnicos.Remove(tecnico);
            await _context.SaveChangesAsync();

            return tecnico;
        }

        private bool TecnicoExists(int id)
        {
            return _context.Tecnicos.Any(e => e.Id_funcionario == id);
        }
    }
}
