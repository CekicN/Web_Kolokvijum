using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class Context:DbContext
    {
        public Context(DbContextOptions option): base(option){}

        public DbSet<Biljka> Biljke { get; set; }
        public DbSet<Cvet> Cvece { get; set; }
        public DbSet<List> Listovi { get; set; }
        public DbSet<Podrucje> Podrucja { get; set; }
        public DbSet<Stablo> Stabla { get; set; }
        public DbSet<NoveBiljke> NoveBiljke { get; set; }
        
    }
}