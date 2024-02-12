using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ENations.Models
{
    public class LawProposal
    {
        [Key]
        public int LawProposalId { get; set; }
        public int ExpectedVotes { get; set; }
        public string Type { get; set; }
        public string Reason { get; set; }
        public decimal Amount { get; set; }
        public bool Yes { get; set; }
        public bool No { get; set; }
        public bool Finished { get; set; }

        public int CongressMemberId { get; set; }
        [ForeignKey("CongressMemberId")]
        public virtual CongressMember CongressMember { get; set; }
    }
}
