using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class AerodromContext:DbContext
    {
        public AerodromContext(DbContextOptions options) : base(options){}

        public DbSet<Aerodrom> Aerodromi { get; set; }
        public DbSet<Letelica> Letelice { get; set; }
        public DbSet<Letovi> Letovi { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Aerodrom>()
                    .HasMany<Letovi>(p => p.LetoviA)
                    .WithOne(p => p.TackaA);

            modelBuilder.Entity<Aerodrom>()
                    .HasMany<Letovi>(p => p.LetoviB)
                    .WithOne(p => p.TackaB);
        }
    }
}