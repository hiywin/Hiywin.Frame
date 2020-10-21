using Hiywin.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Role
{
    public class GetRolePageViewModel
    {
        public string RoleNo { get; set; }
        public string RoleName { get; set; }
        public string AppNo { get; set; }
        public bool? IsDelete { get; set; }
        [Required]
        public PageModel PageModel { get; set; }
    }
}
