using Hiywin.Dtos.Frame;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Group
{
    public class GroupRoleSaveOrUpdateViewModel
    {
        [Required]
        public string GroupNo { get; set; }
        [Required]
        public string AppNo { get; set; }
        [Required]
        public List<SysGroupRoleDto> LstGroupRole { get; set; }
    }
}
