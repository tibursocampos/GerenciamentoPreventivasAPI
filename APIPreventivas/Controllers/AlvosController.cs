using APIPreventivas.Domain.Models;
using APIPreventivas.Models;
using APIPreventivas.Service;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

namespace APIPreventivas.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlvosController : ControllerBase
    {
        private IAlvoService alvoService;

        public AlvosController(IAlvoService alvoService)
        {
            this.alvoService = alvoService;
        }

        // GET: api/Alvos
        [HttpGet]
        public List<Alvo> GetAlvos()
        {
            return alvoService.getAlvos();
        }

        // GET: api/Alvos/5
        [HttpGet("{id}")]
        public ActionResult<Alvo> GetAlvo(int id)
        {
            var alvo = alvoService.getAlvo(id);

            if (alvo == null)
            {
                return NotFound();
            }

            return alvo;
        }

        // GET: api/Alvos/busca?idCronograma=2
        [HttpGet("busca")]
        public ActionResult<List<Alvo>> GetAlvoCronograma(int idCronograma)
        {
            var alvo = alvoService.GetAlvoCronograma(idCronograma);

            if (alvo == null)
            {
                return NotFound();
            }

            return alvo;
        }

        // GET: api/Alvos/alvosAdd
        [HttpGet("alvosAdd/{id}")]
        public ActionResult<Array> GetAlvosTelaAdd(int id)
        {
            var cronogramaDetalhe = alvoService.GetAlvosTelaAdd(id);

            if (cronogramaDetalhe == null)
            {
                return NotFound();
            }

            return cronogramaDetalhe;
        }

        // GET: api/Alvos/alvosConcluidos
        [HttpGet("alvosConcluidos/{idCronograma}")]
        public ActionResult<Array> ListaAlvoConcluidos(int idCronograma)
        {
            var alvosConcluidos = alvoService.ListaAlvoConcluidos(idCronograma);
            return alvosConcluidos;
        }

        // GET: api/Alvos/alvosRestantes
        [HttpGet("alvosRestantes/{idCronograma}")]
        public ActionResult<Array> ListaAlvoRestantes(int idCronograma)
        {
            var alvosRestantes = alvoService.ListaAlvoRestantes(idCronograma);
            return alvosRestantes;
        }

        // PUT: api/Alvos/5
        [HttpPut("{id}")]
        public ActionResult PutAlvo(int id, Alvo alvo)
        {
            if (id != alvo.IdAlvo)
            {
                return BadRequest();
            }                     

            try
            {
                alvoService.PutAlvo(alvo);
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
        [EnableCors]
        [HttpPost]
        public ActionResult<Alvo> PostAlvo(Alvo alvo)
        {
            var alvoCriado = alvoService.PostAlvo(alvo);
            
            return CreatedAtAction("GetAlvo", new { id = alvoCriado.IdAlvo }, alvoCriado);
        }

        // DELETE: api/Alvos/5
        [HttpDelete("{id}")]
        public ActionResult<Alvo> DeleteAlvo(int id)
        {
            var alvo = alvoService.DeleteAlvo(id);
            if (alvo == null)
            {
                return NotFound();
            }

            return alvo;
        }

        private bool AlvoExists(int id)
        {
            return alvoService.AlvoExists(id);
        }
    }
}