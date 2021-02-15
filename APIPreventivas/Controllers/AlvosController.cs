using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIPreventivas.Models;
using APIPreventivas.Service;
using APIPreventivas.Domain.Models;
using static APIPreventivas.Domain.Enum.TipoAtividadeEnum;

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

        // GET: api/Alvos/busca?idCronograma=2
        [HttpGet("busca")]
        public async Task<ActionResult<IEnumerable<Alvo>>> GetAlvoCronograma(int idCronograma)
        {
            var alvo = await _context.Alvos.Where(a => a.IdCronograma == idCronograma).ToListAsync();

            if (alvo == null)
            {
                return NotFound();
            }

            return alvo;
        }

        // GET: api/Alvos/alvosAdd
        [HttpGet("alvosAdd/{id}")]
        public async Task<ActionResult<object>> GetAlvosTelaAdd(int id)
        {
            var cronogramaDetalhe = await AlvoService.GetAlvosTelaAdd(id);

            if (cronogramaDetalhe == null)
            {
                return NotFound();
            }

            return cronogramaDetalhe;
        }

        //// GET: api/Alvos/MGPSO_0001
        //[HttpGet("{endId}")]
        //public async Task<ActionResult<Alvo>> GetAlvoEndId(string EndId)
        //{
        //    var alvo = await _context.Alvos.FindAsync(EndId);

        //    if (alvo == null)
        //    {
        //        return NotFound();
        //    }

        //    return alvo;
        //}

        // PUT: api/Alvos/5
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
        [HttpPost]
        public async Task<ActionResult<Alvo>> PostAlvo(Alvo alvo)
        {
            alvo.Atividades = new List<Atividade>();
            List<Atividade> list = new List<Atividade>();

            Atividade um = new Atividade(TipoAtividade.Aterramento);
            list.Add(um);
            Atividade dois = new Atividade(TipoAtividade.Baterias);
            list.Add(dois);
            Atividade tres = new Atividade(TipoAtividade.Infraestrutura);
            list.Add(tres);
            Atividade quatro = new Atividade(TipoAtividade.Acesso);
            list.Add(quatro);
            Atividade cinco = new Atividade(TipoAtividade.MW);
            list.Add(cinco);
            
            foreach(var lista in list)
            {
                alvo.Atividades.Add(lista);
            }                       
            
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