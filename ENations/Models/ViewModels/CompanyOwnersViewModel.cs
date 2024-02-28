using System.Collections.Generic;

namespace ENations.Models.ViewModels
{
    public class CompanyOwnersViewModel
    {
        public int CompanyId { get; set; }
        public string Type { get; set; }
        public List<UserBasicInfo> Owners { get; set; } = new List<UserBasicInfo>();
    }

    public class UserBasicInfo
    {
        public int UserId { get; set; }
        public string Username { get; set; }
    }
}
