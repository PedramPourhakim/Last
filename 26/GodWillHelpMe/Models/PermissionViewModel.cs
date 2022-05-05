using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GodWillHelpMe.Models
{
    public class PermissionViewModel
    {
        public string RoleId { get; set; }
        public IList<RoleClaimsViewModel> RoleClaims { get; set; }
    }
}
