using Microsoft.EntityFrameworkCore;

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
            optionsBuilder.UseSqlServer("Data Source=RAPHAEL-DESKTOP;" + "Initial Catalog=APIPreventivasNew;Integrated Security=True");
        }

        public DbSet<Supervisor> Supervisores { get; set; }
        public DbSet<Tecnico> Tecnicos { get; set; }
        public DbSet<TecnicoAlvo> TecnicosAlvos { get; set; }
        public DbSet<Alvo> Alvos { get; set; }
        public DbSet<Cronograma> Cronogramas { get; set; }
        public DbSet<Site> Sites { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Supervisor>()
                .HasKey(s => s.Id_funcionario);

            modelBuilder.Entity<Cronograma>()
                .HasOne(s => s.Supervisores)
                .WithMany(c => c.Cronogramas)
                .HasForeignKey(s => s.Id_supervisor);

            modelBuilder.Entity<Tecnico>()
                .HasKey(t => t.Id_funcionario);

            modelBuilder.Entity<TecnicoAlvo>()
                .HasKey(ta => new { ta.Id_alvo, ta.Id_tecnico });

            modelBuilder.Entity<TecnicoAlvo>()
                .HasOne(a => a.Alvo)
                .WithMany(ta => ta.Tecnicos_alvos)
                .HasForeignKey(a => a.Id_alvo);

            modelBuilder.Entity<TecnicoAlvo>()
                .HasOne(t => t.Tecnico)
                .WithMany(ta => ta.Tecnicos_alvos)
                .HasForeignKey(t => t.Id_tecnico);

            modelBuilder.Entity<Alvo>()
                .HasKey(a => a.Id_alvo);

            modelBuilder.Entity<Alvo>()
                .Property(a => a.Site_end_id)
                .IsRequired();

            modelBuilder.Entity<Alvo>()
                .HasOne(c => c.Cronogramas)
                .WithMany(a => a.Alvos)
                .HasForeignKey(c => c.Id_cronograma);

            modelBuilder.Entity<Site>()
                .HasKey(s => s.End_id);

            modelBuilder.Entity<Alvo>()
                .HasOne(s => s.Sites)
                .WithMany(a => a.Alvos)
                .HasForeignKey(s => s.Site_end_id);

            modelBuilder.Entity<Cronograma>()
                .HasKey(c => c.Id_cronograma);
                
                
        }
    }
}
