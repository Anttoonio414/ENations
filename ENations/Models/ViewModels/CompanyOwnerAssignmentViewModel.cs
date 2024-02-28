using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ENations.Models.ViewModels
{
    public class CompanyOwnerAssignmentViewModel
    {
        public List<CompanyOwnersViewModel> CompaniesWithOwners { get; set; } = new List<CompanyOwnersViewModel>();
        public SelectList AllCompanies { get; set; }
        public SelectList AllUsers { get; set; }
        public int SelectedCompanyId { get; set; }
        public int SelectedUserId { get; set; }
    }
}
