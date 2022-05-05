using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using GodWillHelpMe.Helpers;
using GodWillHelpMe.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace GodWillHelpMe.Pages
{
    [Authorize(Roles ="SuperAdmin")]
    public class PermissionModel : PageModel
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public PermissionModel(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }

        public async Task OnGet(string roleId)
        {
            var model = new PermissionViewModel();
            var AllPermissions = new List<RoleClaimsViewModel>();
            AllPermissions.GetPermissions(typeof(Permissions.Permissions.Products), roleId);
            var role = await roleManager.FindByIdAsync(roleId);
            model.RoleId = roleId;
            var claims = await roleManager.GetClaimsAsync(role);
            var allClaimValues = AllPermissions.Select(a => a.Value).ToList();
            var roleClaimValues = claims.Select(a => a.Value).ToList();
            var authorizedClaims = allClaimValues.Intersect(roleClaimValues).ToList();
            foreach (var permission in AllPermissions)
            {
                if (authorizedClaims.Any(a => a == permission.Value))
                {
                    permission.Selected = true;
                }
            }
            model.RoleClaims = AllPermissions;
        }
        public async Task<IActionResult> OnPostUpdate(PermissionViewModel model)
        {
            var role = await roleManager.FindByIdAsync(model.RoleId);
            var claims = await roleManager.GetClaimsAsync(role);
            foreach (var claim in claims)
            {
                await roleManager.RemoveClaimAsync(role, claim);
            }
            var selectedClaims = model.RoleClaims.Where(a => a.Selected).ToList();
            foreach (var claim in selectedClaims)
            {
                await roleManager.AddPermissionClaim(role, claim.Value);
            }
            return RedirectToPage("Permission", new { roleId = model.RoleId });
        }
    }
}
