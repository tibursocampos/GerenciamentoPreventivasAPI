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
    [Route("api/cronogramas")]
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

        //GET: api/Cronogramas/1
        [HttpGet("busca")]
        public async Task<ActionResult<IEnumerable<Cronograma>>> GetCronogramaMes(int mes)
        {
            var cronograma = await _context.Cronogramas.Where(c => (int)c.Mes == mes).ToListAsync();

            if (cronograma == null)
            {
                return NotFound();
            }

            return cronograma;

            //    IQueryable<Cronograma> consulta = _context.Cronogramas;

            //    if (!string.IsNullOrEmpty(mes))
            //    {
            //        consulta = consulta.Where(e => e.Mes.Contains(mes));
            //    }

            //    if (consulta == null)
            //    {
            //        return NotFound(new { mensagem = "Usuário não encontrado !!!" });
            //    }

            //    return await consulta.ToListAsync();
         }

        // GET: api/Cronogramas/detalhes/2
        [HttpGet("detalhes/{id}")]
        public async Task<ActionResult<object>> GetCronogramaDetalhes(int id)
        {
            var cronogramaDetalhe = await CronogramaService.GetAlvosDetalhados(id);

            if (cronogramaDetalhe == null)
            {
                return NotFound();
            }

            return cronogramaDetalhe;
        }

        // PUT: api/Cronogramas/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutCronograma(int id, Cronograma cronograma)
        {
            if (id != cronograma.IdCronograma)
            {
                return BadRequest();
            }

            _context.Entry(cronograma).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                CronogramaService.AlteraStatusCronograma(cronograma);
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
        [HttpPost]
        public async Task<ActionResult<Cronograma>> PostCronograma(Cronograma cronograma)
        {
            _context.Cronogramas.Add(cronograma);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetCronograma", new { id = cronograma.IdCronograma }, cronograma);
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
            return _context.Cronogramas.Any(e => e.IdCronograma == id);
        }
    }
}
