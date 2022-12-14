using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Podrucje
    {
        [Key]
        public int ID { get; set; }
        [MaxLength(50)]
        public string Naziv{ get; set; }

        [JsonIgnore]
        public List<Biljka> Biljke { get; set; }
        [JsonIgnore]
        public List<NoveBiljke> NoveBiljke { get; set; }
    }
}