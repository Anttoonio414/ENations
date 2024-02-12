using System.ComponentModel.DataAnnotations;

namespace ENations.Models
{
    public class Chat
    {
        [Key]
        public int ChatId { get; set; }
        public string ChannelType { get; set; }
        public string ChannelId { get; set; }

        public virtual ICollection<Message> Messages { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
