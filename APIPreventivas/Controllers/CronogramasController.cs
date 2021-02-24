using APIPreventivas.Models;
using APIPreventivas.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace APIPreventivas.Controllers
{
    [Route("api/cronogramas")]
    [ApiController]
    public class CronogramasController : ControllerBase
    {
        private ICronogramaService cronogramaService;
        public CronogramasController(ICronogramaService cronogramaService)
        {
            this.cronogramaService = cronogramaService;
        }

        // GET: api/Cronogramas
        [HttpGet]
        public List<Cronograma> GetCronogramas()
        {
            return cronogramaService.GetCronogramas();
        }

        // GET: api/Cronogramas/5
        [HttpGet("{id}")]
        public ActionResult<Cronograma> GetCronograma(int id)
        {
            var cronograma = cronogramaService.GetCronograma(id);
            
            if (cronograma == null)
            {
                return NotFound();
            }

            return cronograma;
        }

        //GET: api/Cronogramas/1
        [HttpGet("busca")]
        public ActionResult<List<Cronograma>> GetCronogramaMes(int mes)
        {
            var cronograma = cronogramaService.GetCronogramaMes(mes);

            if (cronograma == null)
            {
                return NotFound();
            }

            return cronograma;

         }

        // GET: api/Cronogramas/detalhes/2
        [HttpGet("detalhes/{id}")]
        public ActionResult<object> GetCronogramaDetalhes(int id)
        {
            var cronogramaDetalhe = cronogramaService.GetAlvosDetalhados(id);

            if (cronogramaDetalhe == null)
            {
                return NotFound();
            }

            return cronogramaDetalhe;
        }

        // PUT: api/Cronogramas/5
        [HttpPut("{id}")]
        public ActionResult PutCronograma(int id, Cronograma cronograma)
        {
            if (id != cronograma.IdCronograma)
            {
                return BadRequest();
            }            

            try
            {
                cronogramaService.AlteraStatusCronograma(cronograma);
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
        public ActionResult<Cronograma> PostCronograma(Cronograma cronograma)
        {
            var cronogramaCriado = cronogramaService.PostCronograma(cronograma);

            return CreatedAtAction("GetCronograma", new { id = cronogramaCriado.IdCronograma }, cronogramaCriado);
        }

        // DELETE: api/Cronogramas/5
        [HttpDelete("{id}")]
        public ActionResult<Cronograma> DeleteCronograma(int id)
        {
            var cronograma = cronogramaService.DeleteCronograma(id);
            if (cronograma == null)
            {
                return NotFound();
            }

            return cronograma;
        }

        public bool CronogramaExists(int id)
        {
            return cronogramaService.CronogramaExists(id);
        }
    }
}
