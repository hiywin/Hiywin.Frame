using Hiywin.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Role
{
    public class GetRoleModulePageViewModel
    {
        [Required]
        public string RoleNo { get; set; }
        public string ModuleName { get; set; }
        [Required]
        public PageModel PageModel { get; set; }
    }
}
