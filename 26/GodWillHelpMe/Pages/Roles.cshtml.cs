using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace GodWillHelpMe.Pages
{
    [Authorize(Roles="SuperAdmin")]
    public class RolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> role;
        
        public RolesModel(RoleManager<IdentityRole> role)
        {
            this.role = role;
        }
        public async Task OnGetAsync()
        {
            var roles = await role.Roles.ToListAsync();
        }
        public async Task<IActionResult> AddRole(string roleName)
        {
            if (roleName != null)
            {
                await role.CreateAsync(new IdentityRole(roleName.Trim()));
            }
            return RedirectToPage("/Roles");
        }
    }
}
