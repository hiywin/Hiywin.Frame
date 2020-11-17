using Hiywin.Dtos.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Dtos.Structs
{
    public class SysUserRoleParams
    {
        public string UserNo { get; set; }
        public string AppNo { get; set; }
        public List<SysUserRoleDto> LstUserRole { get; set; }
    }
}
