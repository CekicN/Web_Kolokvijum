using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class Roba
    {
        [Key]
        public int ID { get; set; }

        public required int Zapremina { get; set; }

        public int Tezina { get; set; }
        public DateTime datumPrijema { get; set; }
        public DateTime datumIsporuke { get; set; }
        public int CenaOd { get; set; }
        public int CenaDo { get; set; }

        public Kompanija Kompanija { get; set; }

    }
}