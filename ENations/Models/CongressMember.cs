using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ENations.Models
{
    public class CongressMember
    {
        [Key]
        public int CongressMemberId { get; set; }

        public virtual ICollection<LawProposal> LawProposals { get; set; }

        public int PoliticalPartyId { get; set; }
        [ForeignKey("PoliticalPartyId")]
        public virtual PoliticalParty PoliticalParty { get; set; }
    }
}
