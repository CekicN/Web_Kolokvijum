using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Spoj
    {
        [Key]
        public int ID { get; set; }

        [Range(5, 10)]
        public int Ocena { get; set; }

        public virtual IspitniRok IspitniRok { get; set; }

        [JsonIgnore]
        public virtual Student Student { get; set; }

        public virtual Predmet Predmet { get; set; }
    }
}
