using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Role
{
    public class RoleSaveOrUpdateViewModel
    {
        public string RoleNo { get; set; }
        [Required]
        public string RoleName { get; set; }
        public string Descr { get; set; }
        [Required]
        public string AppNo { get; set; }
        public bool Access { get; set; }
        public bool IsDelete { get; set; }
    }
}
