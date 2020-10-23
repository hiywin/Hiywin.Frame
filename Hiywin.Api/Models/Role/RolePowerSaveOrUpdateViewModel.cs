using Hiywin.Dtos.Frame;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Hiywin.Api.Models.Role
{
    public class RolePowerSaveOrUpdateViewModel
    {
        [Required]
        public string RoleNo { get; set; }
        [Required]
        public List<SysRolePowerDto> LstRolePower { get; set; }
    }
}
