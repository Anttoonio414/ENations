using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ENations.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }
        public int Strength { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int Level { get; set; }
        public int Xp { get; set; }

        public int RegionId { get; set; }

        [ForeignKey("RegionId")]
        public virtual Region Region { get; set; }

        public virtual UserMoney UserMoney { get; set; }

        public virtual UserGyms UserGyms { get; set; }

        public virtual ICollection<UserItems> UserItems { get; set; }

        [InverseProperty("Owners")]
        public virtual ICollection<Company> OwnedCompanies { get; set; }

        [InverseProperty("Employees")]
        public virtual ICollection<Company> EmployedCompanies { get; set; }

        public virtual ICollection<Article> Articles { get; set; }

        public virtual ICollection<LawProposal> LawProposals { get; set; }

        public virtual ICollection<PartyMember> PartyMemberships { get; set; }
    }
}
