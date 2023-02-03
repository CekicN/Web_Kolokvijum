using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Boja
    {
        [Key]
        public int ID { get; set; }
        public string Naziv { get; set; }
        [JsonIgnore]
        public Model Model { get; set; }
    }
}