using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class Context:DbContext
    {
        public Context(DbContextOptions op):base(op){}


        public DbSet<Kompanija> Kompanija { get; set; }
        public DbSet<Roba> Roba { get; set; }
        public DbSet<Vozilo> Vozilo { get; set; }
    } 
}