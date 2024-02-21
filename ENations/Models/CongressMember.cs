using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENations.Models
{
    public class CongressMember
    {
        [Key]
        public int CongressMemberId { get; set; }

        public virtual ICollection<LawProposal> LawProposals { get; set; }

        public int PartyMemberId { get; set; }

        [ForeignKey("PartyMemberId")]
        public virtual PartyMember PartyMember { get; set; }
    }
}
