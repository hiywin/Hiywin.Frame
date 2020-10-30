using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.IFrameService.Structs
{
    public class SysGroupQuery
    {
        public string GroupNo { get; set; }
        public string GroupName { get; set; }
        public string Code { get; set; }
        public string ParentNo { get; set; }
        public string AppNo { get; set; }
        public bool IsDelete { get; set; }
    }
}
