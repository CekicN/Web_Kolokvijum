using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Fotografija
    {
        [Key]
        public int ID { get; set; }

        public string Naziv { get; set; }
        public Papir Papir { get; set; }
        public Dimenzija Dimenzija { get; set; }
        public Ram Ram { get; set; }
        
    }
}