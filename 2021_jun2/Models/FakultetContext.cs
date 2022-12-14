using Microsoft.EntityFrameworkCore;

namespace Models
{
    public class FakultetContext : DbContext
    {
        public DbSet<Student> Studenti { get; set; }
        public DbSet<Predmet> Predmeti { get; set; }
        public DbSet<IspitniRok> Rokovi { get; set; }
        public DbSet<Spoj> StudentiPredmeti { get; set; }

        public FakultetContext(DbContextOptions options) : base(options)
        {}

    }
}