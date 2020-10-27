using Hiywin.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Module
{
    public class GetModulePageViewModel
    {
        public string ModuleNo { get; set; }
        public string ModuleName { get; set; }
        public string ParentNo { get; set; }
        public string AppNo { get; set; }
        public bool? IsDelete { get; set; }
        [Required]
        public PageModel PageModel { get; set; }
    }
}
