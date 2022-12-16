using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Letelica
    {
        [Key]
        public int ID { get; set; }
        public string Naziv { get; set; }
        public int KapacitetPutnika { get; set; }
        public int Posada { get; set; }
        public int BrMotora { get; set; }
        [JsonIgnore]
        public List<Letovi> Let { get; set; }
    }
}