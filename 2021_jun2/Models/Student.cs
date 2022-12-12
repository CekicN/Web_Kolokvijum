using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
namespace Models
{
    [Table("Student")]
    public class Student
    {
        [Key]
        public int ID { get; set; }

        [Required]
        [Range(10000,20000)]
        public int Indeks { get; set; }
 
        [RegularExpression("^[a-zA-Z0-9]+$")]
        [Required]
        [MaxLength(50)]
        public string Ime { get; set; }
        [Required]
        [MaxLength(50)]
        public string Prezime { get; set; }

        public List<Spoj> StudentPredmet { get; set; }
    }
}