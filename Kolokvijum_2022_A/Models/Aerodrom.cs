using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Aerodrom
    {
        [Key]
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string Code { get; set; }
        public string Lokacija { get; set; }

        public int KapacitetLetelica { get; set; }
        public int KapacitetPutnika { get; set; }

        [JsonIgnore]
        public List<Letovi> LetoviA { get; set; }
        [JsonIgnore]
        public List<Letovi> LetoviB { get; set; }
    }
}