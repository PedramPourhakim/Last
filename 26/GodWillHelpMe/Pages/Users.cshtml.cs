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
    [Authorize]
    public class UsersModel : PageModel
    {
        
        private readonly UserManager<IdentityUser> userManager;
        
        public UsersModel(UserManager<IdentityUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task OnGetAsync()
        {
            var CurrentUser =await userManager.GetUserAsync(HttpContext.User);
            var allUserExceptCurrentUser = await userManager.Users.Where(a => a.Id != CurrentUser.Id).ToListAsync();
        }
    }
}
