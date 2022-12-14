using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class NoveBiljke
    {
        [Key]
        public int ID { get; set; }

        public Cvet Cvet { get; set; }
        public Podrucje Podrucje { get; set; }
        public List List { get; set; }
        public Stablo Stablo { get; set; }

    }
}