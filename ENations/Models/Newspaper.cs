using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ENations.Models
{
    public class Newspaper
    {
        [Key]
        public int NewspaperId { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public int UserId { get; set; }

        [ForeignKey("UserId")]
        public virtual User Founder { get; set; }

        public virtual ICollection<Article> Articles { get; set; }
    }
}
