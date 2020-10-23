using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Dtos.Frame
{
    public class SysModuleTreeDto
    {
        public string ModuleNo { get; set; }
        public string ModuleName { get; set; }
        public string ParentNo { get; set; }
        public string Icon { get; set; }
        public string AppNo { get; set; }
        public int Sort { get; set; }
        public bool IsDelete { get; set; }

        public List<SysModuleTreeDto> Children { get; set; }
    }
}
