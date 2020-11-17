using Hiywin.Dtos.Frame;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.User
{
    public class UserRoleSaveOrUpdateViewModel
    {
        [Required]
        public string UserNo { get; set; }
        [Required]
        public string AppNo { get; set; }
        [Required]
        public List<SysUserRoleDto> LstUserRole { get; set; }
    }
}
