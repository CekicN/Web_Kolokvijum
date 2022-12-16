using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Kompanija
    {
        [Key]
        public int ID { get; set; }
        public string Naziv { get; set; }

        public int ProsecnaZarada { get; set; }
        public int BrojDanaZaIsporuku { get; set; }
        public int Cena { get; set; }
        [JsonIgnore]
        public List<Vozilo> Vozila { get; set; }

        [JsonIgnore]
        public List<Roba> Robe { get; set; }

    }
}