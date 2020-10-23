using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Role
{
    public class RoleModuleSaveOrUpdateViewModel
    {
        [Required]
        public string RoleNo { get; set; }
        [Required]
        public List<string> LstModuleNo { get; set; }
    }
}
