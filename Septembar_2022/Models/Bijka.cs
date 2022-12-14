using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Biljka
    {
        [Key]
        public int ID { get; set; }
        public int kolicina { get; set; }
        public string Naziv { get; set; }

        public Cvet Cvet { get; set; }
        public Podrucje Podrucje { get; set; }
        public List List { get; set; }
        public Stablo Stablo { get; set; }
    }
}