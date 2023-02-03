using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Marka
    {
        [Key]
        public int ID { get; set; }
        public string Naziv { get; set; }
        [JsonIgnore]
        public List<Model> Modeli { get; set;}
        
    }
}