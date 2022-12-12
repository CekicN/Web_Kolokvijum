using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class IspitniRok
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Naziv { get; set; }

        [JsonIgnore]
        public List<Spoj> StudentiPredmeti { get; set; }

    }
}