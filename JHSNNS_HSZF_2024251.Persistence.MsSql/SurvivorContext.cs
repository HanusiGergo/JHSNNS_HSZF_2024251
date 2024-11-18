using Microsoft.EntityFrameworkCore;
using JHSNNS_HSZF_2024251.Model;

namespace JHSNNS_HSZF_2024251.Persistence.MsSql
{
    public class SurvivorContext : DbContext
    {
        public SurvivorContext(DbContextOptions<SurvivorContext> options) : base(options) { }

        public DbSet<Survivor> Survivors { get; set; } = null!;
        public DbSet<SurvivorTask> Tasks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Példa konfigurációra
            modelBuilder.Entity<Survivor>()
                .HasMany(s => s.Tasks)
                .WithOne(t => t.Survivor)
                .HasForeignKey(t => t.SurvivorId);

            base.OnModelCreating(modelBuilder);
        }
    }
}
