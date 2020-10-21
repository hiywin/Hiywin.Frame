using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Role
{
    public class GetRoleAllViewModel
    {
        public string RoleNo { get; set; }
        public string RoleName { get; set; }
        public string AppNo { get; set; }
        public bool? IsDelete { get; set; }
    }
}
