using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Module
{
    public class ModuleSaveOrUpdateViewModel
    {
        public string ModuleNo { get; set; }
        public string ModuleName { get; set; }
        public string ParentNo { get; set; }
        public string Icon { get; set; }
        public string Url { get; set; }
        public int Category { get; set; }
        public string Target { get; set; }
        public int IsResource { get; set; }
        public int App { get; set; }
        public bool IsDelete { get; set; }
        public int Sort { get; set; }
    }
}
