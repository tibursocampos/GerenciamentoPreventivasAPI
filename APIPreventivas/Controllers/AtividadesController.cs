using APIPreventivas.Domain.Models;
using APIPreventivas.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace APIPreventivas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtividadesController : ControllerBase
    {
        private IAtividadeService atividadeService;

        public AtividadesController(IAtividadeService atividadeService)
        {
            this.atividadeService = atividadeService;
        }

        // GET: api/Atividades
        [HttpGet]
        //public async Task<ActionResult<IEnumerable<Atividade>>> GetAtividades()
        public List<Atividade> GetAtividades()
        {
            return atividadeService.GetAtividades();
        }

        // GET: api/Atividades/5
        [HttpGet("{id}")]
        //public async Task<ActionResult<Atividade>> GetAtividade(int id)
        public ActionResult<Atividade> GetAtividade(int id)
        {
            var atividade = atividadeService.GetAtividade(id);

            if (atividade == null)
            {
                return NotFound();
            }

            return atividade;
        }

        // GET: api/Atividades/todas/cronograma?idCronograma=2
        [HttpGet("todas/cronograma")]
        public ActionResult<List<Atividade>> GetTodasAtividadesCronograma(int idCronograma)
        {
            var atividades = atividadeService.TodasAtividadesCronograma(idCronograma);
            if (atividades == null)
            {
                return NotFound();
            }
            return atividades;
        }

        // GET: api/Atividades/concluidas/cronograma?idCronograma=2
        [HttpGet("concluidas/cronograma")]
        public ActionResult<List<Atividade>> GetAtividadesConcluidas(int idCronograma)
        {
            var atividades = atividadeService.AtividadesConcluidas(idCronograma);
            if(atividades == null)
            {
                return NotFound();
            }
            return atividades;
        }

        // GET: api/Atividades/programadas/cronograma?idCronograma=2
        [HttpGet("programadas/cronograma")]
        public ActionResult<List<Atividade>> GetAtividadesProgramadas(int idCronograma)
        {
            var atividades = atividadeService.AtividadesProgramadas(idCronograma);
            if (atividades == null)
            {
                return NotFound();
            }
            return atividades;
        }

        // PUT: api/Atividades/5        
        [HttpPut("{id}")]
        public ActionResult PutAtividade(int id, Atividade atividade)
        {
            if (id != atividade.IdAtividade)
            {
                return BadRequest();
            }


            try
            {
                atividadeService.AlteraStatusAlvo(atividade);              
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AtividadeExists(id))
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

        // POST: api/Atividades
        [HttpPost]
        public ActionResult<Atividade> PostAtividade(Atividade atividade)
        {
            var atividadeCriada = atividadeService.PostAtividade(atividade);            

            return CreatedAtAction("GetAtividade", new { id = atividadeCriada.IdAtividade }, atividadeCriada);
        }

        // DELETE: api/Atividades/5
        [HttpDelete("{id}")]
        public ActionResult<Atividade> DeleteAtividade(int id)
        {
            var atividade = atividadeService.DeleteAtividade(id);
            if (atividade == null)
            {
                return NotFound();
            }           

            return atividade;
        }

        private bool AtividadeExists(int id)
        {
            return atividadeService.AtividadeExists(id);
        }
    }
}
