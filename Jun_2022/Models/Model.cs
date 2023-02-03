using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Model
    {
        [Key]
        public int ID { get; set; }
        public string Naziv { get; set; }
        [JsonIgnore]
        public Marka Marka { get; set; }
        [JsonIgnore]
        public List<Boja> Boje { get; set; }
    }
}