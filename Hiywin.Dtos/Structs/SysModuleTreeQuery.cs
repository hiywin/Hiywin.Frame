using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Dtos.Structs
{
    public class SysModuleTreeQuery
    {
        public string ModuleName { get; set; }
        public string ParentNo { get; set; }
        public int App { get; set; }
    }
}
