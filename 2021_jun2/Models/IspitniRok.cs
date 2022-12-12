using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class IspitniRok
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Naziv { get; set; }
        public List<Spoj> StudentiPredmeti { get; set; }

    }
}