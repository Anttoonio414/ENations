using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ENations.Models
{
    public class UserMoney
    {
        [Key, ForeignKey("User")]
        public int UserId { get; set; }
        public decimal Gold { get; set; }
        public string Currency { get; set; }

        public virtual User User { get; set; }
    }
}
