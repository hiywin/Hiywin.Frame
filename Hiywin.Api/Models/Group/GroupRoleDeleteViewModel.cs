using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Group
{
    public class GroupRoleDeleteViewModel
    {
        [Required]
        public string GroupNo { get; set; }
        [Required]
        public string RoleNo { get; set; }
    }
}
