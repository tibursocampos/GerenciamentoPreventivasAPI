using APIPreventivas.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace APIPreventivas.Service
{
    public interface ISiteService
    {
        List<Site> GetSites();
        Site GetSite(int idSite);
        List<Site> GetSiteByEndId(string endId);
        Site PostSite(Site site);
        void PutSite(Site site);
        Site DeleteSite(int idSite);
        bool SiteExists(int idSite);

    }
    public class SiteService : ISiteService
    {
        private readonly APIPreventivaContext db;

        public SiteService(APIPreventivaContext context)
        {
            db = context;
        }

        public List<Site> GetSites()
        {
            return db.Sites.OrderBy(s => s.Cidade).ToList();
        }

        public Site GetSite(int idSite)
        {
            var site = db.Sites.Find(idSite);
            return site;
        }

        public List<Site> GetSiteByEndId(string endId)
        {
            var site = db.Sites.Where(e => e.EndId.Contains(endId)).ToList();

            return site;
        }

        public Site PostSite(Site site)
        {
            db.Sites.Add(site);
            db.SaveChanges();

            return site;
        }

        public void PutSite(Site site)
        {
            db.Entry(site).State = EntityState.Modified;
            db.SaveChanges();
        }

        public Site DeleteSite(int idSite)
        {
            var site = db.Sites.Find(idSite);
            db.Sites.Remove(site);
            db.SaveChanges();

            return site;
        }

        public bool SiteExists(int idSite)
        {
            return db.Sites.Any(e => e.IdSite == idSite);
        }
    }
}

