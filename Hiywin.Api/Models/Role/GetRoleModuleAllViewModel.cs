using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Role
{
    public class GetRoleModuleAllViewModel
    {
        [Required]
        public string RoleNo { get; set; }
        public string ModuleName { get; set; }
    }
}
