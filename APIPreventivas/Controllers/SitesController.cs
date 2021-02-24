using APIPreventivas.Models;
using APIPreventivas.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace APIPreventivas.Controllers
{
    [Route("api/sites")]
    [ApiController]
    public class SitesController : ControllerBase
    {
        private readonly ISiteService siteService;

        public SitesController(ISiteService siteService)
        {
            this.siteService = siteService;
        }

        // GET: api/Sites
        [HttpGet]
        public List<Site> GetSites()
        {
            return siteService.GetSites();
        }

        // GET: api/Sites/5
        [HttpGet("{id}")]
        public ActionResult<Site> GetSite(int id)
        {
            var site = siteService.GetSite(id);

            if (site == null)
            {
                return NotFound();
            }

            return site;
        }

        // GET: api/Sites/MGPSO_0001
        [HttpGet("busca")]
        public ActionResult<List<Site>> GetSiteByEndId(string endId)
        {
            var site = siteService.GetSiteByEndId(endId);

            if (site == null)
            {
                return NotFound(new { mensagem = "Site não encontrado !!!" });
            }

            return site;
        }

        // PUT: api/Sites/5
        [HttpPut("{id}")]
        public ActionResult PutSite(int id, Site site)
        {
            if (id != site.IdSite)
            {
                return BadRequest();
            }

            try
            {
                siteService.PutSite(site);
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
        public ActionResult<Site> PostSite(Site site)
        {
            var alvoCriado = siteService.PostSite(site);

            return CreatedAtAction("GetSite", new { id = alvoCriado.IdSite }, alvoCriado);
        }

        // DELETE: api/Sites/5
        [HttpDelete("{id}")]
        public ActionResult<Site> DeleteSite(int id)
        {
            var site = siteService.DeleteSite(id);
            if (site == null)
            {
                return NotFound();
            }

            return site;
        }

        private bool SiteExists(int id)
        {
            return siteService.SiteExists(id);
        }
    }
}
