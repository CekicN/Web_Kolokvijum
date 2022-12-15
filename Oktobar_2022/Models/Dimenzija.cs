using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Dimenzija
    {
        [Key]
        public int ID { get; set; }

        public required int Visina{ get; set; }
        public required int Sirina { get; set; }

        [JsonIgnore]
        public List<Fotografija> Fotografije { get; set; }
        [JsonIgnore]
        public List<Ram> Ramovi { get; set; }
    }
}