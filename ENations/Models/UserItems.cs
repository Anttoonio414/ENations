using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ENations.Models
{
    public class UserItems
    {
        [Key]
        public int UserItemsId { get; set; }
        public string Item { get; set; }
        public int Quality { get; set; }
        public int Quantity { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User User { get; set; }
    }

}
