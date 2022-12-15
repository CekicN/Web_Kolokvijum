using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class FotoContext:DbContext
    {
        public FotoContext(DbContextOptions op):base(op){}

        public DbSet<Dimenzija> Dimenzije { get; set; }
        public DbSet<Fotografija> Fotografije { get; set; }
        public DbSet<Papir> Papiri { get; set; }
        public DbSet<Ram> Ramovi { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);           
        }
        
    }
}