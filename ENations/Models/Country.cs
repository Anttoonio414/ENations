using System.ComponentModel.DataAnnotations;

namespace ENations.Models
{
    public class Country
    {
        [Key]
        public int CountryId { get; set; }
        public string Capital { get; set; }
        public string Currency { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Region> Regions { get; set; }
        public virtual CountryFunds CountryFunds { get; set; }
        public virtual ICollection<PoliticalParty> PoliticalParties { get; set; }
    }
}
