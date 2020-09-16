using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Module
{
    public class ModuleDeleteViewModel
    {
        [Required]
        public string ModuleNo { get; set; }
        [Required]
        public bool IsDelete { get; set; }
    }
}
