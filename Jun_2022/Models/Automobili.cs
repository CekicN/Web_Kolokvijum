using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Automobili
    {
        [Key]
        public int ID { get; set; }
        public string Marka { get; set; }
        public string Boja {get; set;}
        public string URLSlike { get; set; }
        public string Model { get; set; }
        public int Kolicina { get; set; }
        public DateTime DatumProdaje { get; set; }
        public int Cena { get; set; }
    }
}