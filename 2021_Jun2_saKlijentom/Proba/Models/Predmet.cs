using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models
{
    public class Predmet
    {
        [Key]
        public int ID { get; set; }

        [MaxLength(50)]
        public string Naziv { get; set; }

        [Range(1, 5)]
        public int Godina { get; set; }

        // JsonIgnore se koristi kada ne želimo da dopustimo Json serializer-u da ovaj property serializuje
        [JsonIgnore]
        public virtual List<Spoj> PredmetStudent { get; set; }
    }
}
