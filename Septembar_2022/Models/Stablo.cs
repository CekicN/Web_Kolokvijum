using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Stablo
    {
        [Key]
        public int ID { get; set; }
        public string Izgled { get; set; }


        [JsonIgnore]
        public List<Biljka> Biljke { get; set; }
        [JsonIgnore]
        public List<NoveBiljke> NoveBiljke { get; set; }
    }
}