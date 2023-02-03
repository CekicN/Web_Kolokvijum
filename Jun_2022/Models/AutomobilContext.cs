using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class AutomobilContext:DbContext
    {
        public AutomobilContext(DbContextOptions op):base(op){}

        public DbSet<Boja> Boje { get; set; }
        public DbSet<Marka> Marke { get; set; }
        public DbSet<Model> Modeli { get; set; }
        public DbSet<Automobili> Automobili { get; set; }
    }
}