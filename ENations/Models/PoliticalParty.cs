using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.Metrics;

namespace ENations.Models
{
    public class PoliticalParty
    {
        [Key]
        public int PoliticalPartyId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }

        public int CountryId { get; set; }
        [ForeignKey("CountryId")]
        public virtual Country Country { get; set; }

        public virtual ICollection<PartyMember> PartyMembers { get; set; }
        public virtual ICollection<CongressMember> CongressMembers { get; set; }
    }

}
