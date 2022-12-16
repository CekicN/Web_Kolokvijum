using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Ram
    {
        [Key]
        public int ID { get; set; }

        public required string Materijal { get; set; }
        int BrRamova;


        public Dimenzija Dimenzija { get; set; }
        public List<Fotografija> Fotografije { get; set; }
    }
}