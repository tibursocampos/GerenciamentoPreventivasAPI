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
    public class SupervisoresController : ControllerBase
    {
        private readonly APIPreventivaContext _context;

        public SupervisoresController(APIPreventivaContext context)
        {
            _context = context;
        }

        // GET: api/Supervisores
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Supervisor>>> GetSupervisores()
        {
            return await _context.Supervisores.ToListAsync();
        }

        // GET: api/Supervisores/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Supervisor>> GetSupervisor(int id)
        {
            var supervisor = await _context.Supervisores.FindAsync(id);

            if (supervisor == null)
            {
                return NotFound();
            }

            return supervisor;
        }

        // PUT: api/Supervisores/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSupervisor(int id, Supervisor supervisor)
        {
            if (id != supervisor.Id_funcionario)
            {
                return BadRequest();
            }

            _context.Entry(supervisor).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SupervisorExists(id))
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

        // POST: api/Supervisores
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Supervisor>> PostSupervisor(Supervisor supervisor)
        {
            _context.Supervisores.Add(supervisor);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetSupervisor", new { id = supervisor.Id_funcionario }, supervisor);
        }

        // DELETE: api/Supervisores/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Supervisor>> DeleteSupervisor(int id)
        {
            var supervisor = await _context.Supervisores.FindAsync(id);
            if (supervisor == null)
            {
                return NotFound();
            }

            _context.Supervisores.Remove(supervisor);
            await _context.SaveChangesAsync();

            return supervisor;
        }

        private bool SupervisorExists(int id)
        {
            return _context.Supervisores.Any(e => e.Id_funcionario == id);
        }
    }
}
