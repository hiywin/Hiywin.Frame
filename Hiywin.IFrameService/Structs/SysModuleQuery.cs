using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.IFrameService.Structs
{
    public class SysModuleQuery
    {
        public string ModuleNo { get; set; }
        public string ModuleName { get; set; }
        public string ParentNo { get; set; }
        public bool? IsParentNo { get; set; }
        public string AppNo { get; set; }
        public bool? IsDelete { get; set; }
        public DateTime? StarTime { get; set; }
        public DateTime? EndTime { get; set; }
        public string CreateName { get; set; }
    }
}
