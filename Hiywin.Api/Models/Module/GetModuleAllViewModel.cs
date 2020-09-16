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
    }
}
