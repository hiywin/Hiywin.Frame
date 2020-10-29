using Hiywin.Dtos.Frame;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Position
{
    public class PositionRoleSaveOrUpdateViewModel
    {
        [Required]
        public string PositionNo { get; set; }
        [Required]
        public string AppNo { get; set; }
        [Required]
        public List<SysPositionRoleDto> LstPositionRole { get; set; }
    }
}
