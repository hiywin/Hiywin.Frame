using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.User
{
    public class UserRoleDeleteViewModel
    {
        [Required]
        public string UserNo { get; set; }
        [Required]
        public string RoleNo { get; set; }
    }
}
