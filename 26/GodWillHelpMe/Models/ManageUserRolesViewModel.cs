using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GodWillHelpMe.Models
{
    public class ManageUserRolesViewModel
    {
        public string UserId { get; set; }
        public IList<UserRolesViewModel> UserRoles { get; set; }
    }
}
