using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ENations.Models
{
    public class PartyMember
    {
        [Key]
        public int PartyMemberId { get; set; }
        public int Level { get; set; }

        public int PoliticalPartyId { get; set; }

        [ForeignKey("PoliticalPartyId")]
        public virtual PoliticalParty PoliticalParty { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User User { get; set; }

        public virtual CongressMember CongressMember { get; set; }
    }
}
