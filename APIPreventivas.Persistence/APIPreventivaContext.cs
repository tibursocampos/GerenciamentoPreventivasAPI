using APIPreventivas.Domain.Enum;
using APIPreventivas.Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System;

namespace APIPreventivas.Models
{
    public class APIPreventivaContext : DbContext
    {
        public APIPreventivaContext(DbContextOptions<APIPreventivaContext> options) :base(options)
        {               
        }

        public APIPreventivaContext()
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=RAPHAEL-DESKTOP;" + "Initial Catalog=APIPreventivasNovaModel;Integrated Security=True");
        }
        
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Cronograma> Cronogramas { get; set; }
        public DbSet<Site> Sites { get; set; }
        public DbSet<Alvo> Alvos { get; set; }
        public DbSet<AlvoSite> AlvosSites { get; set; }
        public DbSet<Atividade> Atividades { get; set; }
        
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasKey(t => t.IdUsuario);           

            modelBuilder.Entity<Cronograma>()
                .HasKey(c => c.IdCronograma);

            modelBuilder.Entity<Cronograma>()
                .HasOne(s => s.Supervisores)
                .WithMany(c => c.Cronogramas)
                .HasForeignKey(c => c.IdSupervisor);

            modelBuilder.Entity<Site>()
                .HasKey(s => s.IdSite);

            modelBuilder.Entity<Site>()
                .Property(c => c.IdSite)
                .UseIdentityColumn();

            modelBuilder.Entity<AlvoSite>()
                .HasKey(x => new { x.IdAlvo, x.IdSite });

            modelBuilder.Entity<AlvoSite>()
                .HasOne(a => a.Alvo)
                .WithMany(s => s.AlvosSites)
                .HasForeignKey(a => a.IdAlvo);

            modelBuilder.Entity<AlvoSite>()
                .HasOne(a => a.Site)
                .WithMany(s => s.AlvosSites)
                .HasForeignKey(a => a.IdSite);

            modelBuilder.Entity<Alvo>()
                .HasKey(a => a.IdAlvo); 

            modelBuilder.Entity<Alvo>()
                .HasOne(c => c.Cronogramas)
                .WithMany(a => a.Alvos)
                .HasForeignKey(c => c.IdCronograma);

            modelBuilder.Entity<Atividade>()
                .HasKey(a => a.IdAtividade);

            modelBuilder.Entity<Atividade>()
                .HasOne(a => a.Alvos)
                .WithMany(b => b.Atividades)
                .HasForeignKey(b => b.IdAlvo);

            modelBuilder.Entity<Atividade>()
                .HasOne(t => t.Tecnicos)
                .WithMany(a => a.Atividades)
                .HasForeignKey(a => a.IdTecnico)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
