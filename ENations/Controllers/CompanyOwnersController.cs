using ENations.Models;
using ENations.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace ENations.Controllers
{
    public class CompanyOwnersController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CompanyOwnersController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var viewModel = new CompanyOwnerAssignmentViewModel
            {
                CompaniesWithOwners = await _context.Companies
                    .Select(company => new CompanyOwnersViewModel
                    {
                        CompanyId = company.CompanyId,
                        Type = company.Type,
                        Owners = company.Owners.Select(owner => new UserBasicInfo
                        {
                            UserId = owner.UserId,
                            Username = owner.Username
                        }).ToList()
                    }).ToListAsync(),
                AllCompanies = new SelectList(await _context.Companies.ToListAsync(), "CompanyId", "CompanyId"),
                AllUsers = new SelectList(await _context.Users.ToListAsync(), "UserId", "Username")
            };

            return View(viewModel);
        }


        [HttpPost]
        public async Task<IActionResult> AddOwner(CompanyOwnerAssignmentViewModel model)
        {
                var company = await _context.Companies.Include(c => c.Owners).FirstOrDefaultAsync(c => c.CompanyId == model.SelectedCompanyId);
                var user = await _context.Users.FirstOrDefaultAsync(u => u.UserId == model.SelectedUserId); // Assuming SelectedUserId is of the type that matches your User's ID field

                if (company != null && user != null && !company.Owners.Contains(user))
                {
                    company.Owners.Add(user);
                    await _context.SaveChangesAsync();
                }
            

            return RedirectToAction(nameof(Index));
        }


        [HttpPost]
        public async Task<IActionResult> DeleteOwner(int companyId, int userId)
        {
            var company = await _context.Companies.Include(c => c.Owners).FirstOrDefaultAsync(c => c.CompanyId == companyId);
            var user = company?.Owners.FirstOrDefault(u => u.UserId == userId);

            if (user != null)
            {
                company.Owners.Remove(user);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
