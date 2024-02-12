using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System;

namespace ENations.Models
{
    public class Message
    {
        [Key]
        public int MessageId { get; set; }
        public int Likes { get; set; }

        public int ChatId { get; set; }
        [ForeignKey("ChatId")]
        public virtual Chat Chat { get; set; }

        public int UserId { get; set; }
        [ForeignKey("UserId")]
        public virtual User Sender { get; set; }
    }
}
