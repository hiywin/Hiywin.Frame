using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Module
{
    public class GetModuleAllViewModel
    {
        public string ModuleNo { get; set; }
        public string ModuleName { get; set; }
        public bool? IsDelete { get; set; }
        public string ParentNo { get; set; }
        public bool? IsParentNo { get; set; }
        public int? App { get; set; }
    }
}
