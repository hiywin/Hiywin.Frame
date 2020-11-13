using Hiywin.Dtos.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Dtos.Structs
{
    public class SysGroupRoleParams
    {
        public string GroupNo { get; set; }
        public string AppNo { get; set; }
        public List<SysGroupRoleDto> LstGroupRole { get; set; }
    }
}
