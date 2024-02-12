using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ENations.Models
{
    public class Article
    {
        [Key]
        public int ArticleId { get; set; }
        public int Votes { get; set; }
        public int Views { get; set; }
        public string Category { get; set; }
        public string Text { get; set; }
        public string Title { get; set; }

        public int NewspaperId { get; set; }

        [ForeignKey("NewspaperId")]
        public virtual Newspaper Newspaper { get; set; }
    }
}
