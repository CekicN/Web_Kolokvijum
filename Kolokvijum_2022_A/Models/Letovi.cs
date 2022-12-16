using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Letovi
    {
        [Key]
        public int ID { get; set; }
        public int BrojPutnika { get; set; }
        public DateTime VremePoletanja { get; set; }
        public DateTime VremeSletanja { get; set; }

        public Letelica Letelica { get; set; }
        public Aerodrom TackaA { get; set; }
        public Aerodrom TackaB { get; set; }
        
    }
}