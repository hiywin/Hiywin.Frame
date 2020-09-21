using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Module
{
    public class GetModuleTreeViewModel
    {
        public string ModuleName { get; set; }
        public string ParentNo { get; set; }
        public int App { get; set; }
    }
}
