using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Vozilo
    {
        [Key]
        public int ID { get; set; }

        public int Kolicina { get; set; }

        public Kompanija Kompanija { get; set; }
    }
}