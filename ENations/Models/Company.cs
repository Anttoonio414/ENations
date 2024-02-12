using System.ComponentModel.DataAnnotations;

namespace ENations.Models
{
    public class Company
    {
        [Key]
        public int CompanyId { get; set; }
        public string Type { get; set; }
        public int Quality { get; set; }
        public DateTime LastWorked { get; set; }

        public virtual ICollection<User> Owners { get; set; }

        public virtual ICollection<User> Employees { get; set; }
    }

}
