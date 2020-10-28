using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Position
{
    public class PositionRoleDeleteViewModel
    {
        [Required]
        public string PositionNo { get; set; }
        [Required]
        public string RoleNo { get; set; }
    }
}
