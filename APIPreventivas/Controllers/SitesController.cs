using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using APIPreventivas.Models;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;

namespace APIPreventivas.Controllers
{
    [Route("api/sites")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        private readonly APIPreventivaContext _context;

        public SitesController(APIPreventivaContext context)
        {
            _context = context;
        }

        // GET: api/Sites
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Site>>> GetSites()
        {
            return await _context.Sites.OrderBy(s => s.Cidade).ToListAsync();
        }

        // GET: api/Sites/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Site>> GetSite(int id)
        {
            var site = await _context.Sites.FindAsync(id);

            if (site == null)
            {
                return NotFound();
            }

            return site;
        }

        // GET: api/Sites/MGPSO_0001
        [HttpGet("busca")]
        public async Task<ActionResult<IEnumerable<Site>>> GetSiteByEndId(string endId)
        {
            var site = await _context.Sites.Where(e => e.EndId.Contains(endId)).ToListAsync();

            if (site == null)
            {
                return NotFound(new { mensagem = "Site não encontrado !!!" });
            }

            return site;
        }

        //// GET: api/Sites/PSVG14
        //[HttpGet("{nomeGsm}")]
        //public async Task<ActionResult<Site>> GetSiteBySiteGsm(string nomeGsm)
        //{
        //    var site = await _context.Sites.FindAsync(nomeGsm);

        //    if (site == null)
        //    {
        //        return NotFound();
        //    }

        //    return site;
        //}

        //// GET: api/Sites/MG5014
        //[HttpGet("{nomeWcdma}")]
        //public async Task<ActionResult<Site>> GetSiteBySiteWcdma(string nomeWcdma)
        //{
        //    var site = await _context.Sites.FindAsync(nomeWcdma);

        //    if (site == null)
        //    {
        //        return NotFound();
        //    }

        //    return site;
        //}

        //// GET: api/Sites/Passos
        //[HttpGet("{cidade}")]
        //public async Task<ActionResult<Site>> GetSiteByCidade(string cidade)
        //{
        //    var site = await _context.Sites.FindAsync(cidade);

        //    if (site == null)
        //    {
        //        return NotFound();
        //    }

        //    return site;
        //}

        // PUT: api/Sites/5
        [HttpPut("{id}")]
        public async Task<IActionResult> PutSite(string id, Site site)
        {
            if (id != site.EndId)
            {
                return BadRequest();
            }

            _context.Entry(site).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!SiteExists(id))
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

        // POST: api/Sites
        [HttpPost]
        public async Task<ActionResult<Site>> PostSite(Site site)
        {
            _context.Sites.Add(site);
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (SiteExists(site.EndId))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtAction("GetSite", new { id = site.EndId }, site);
        }

        // DELETE: api/Sites/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Site>> DeleteSite(string id)
        {
            var site = await _context.Sites.FindAsync(id);
            if (site == null)
            {
                return NotFound();
            }

            _context.Sites.Remove(site);
            await _context.SaveChangesAsync();

            return site;
        }

        private bool SiteExists(string id)
        {
            return _context.Sites.Any(e => e.EndId == id);
        }
    }
}
