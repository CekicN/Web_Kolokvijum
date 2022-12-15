using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Papir
    {
        [Key]
        public int ID { get; set; }

        public required string Naziv { get; set; }
        
        public List<Fotografija> foto { get; set; }
    }
}