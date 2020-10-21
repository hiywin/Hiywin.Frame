using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.IFrameService.Structs
{
    public class SysRoleQuery
    {
        public string RoleNo { get; set; }
        public string RoleName { get; set; }
        public string AppNo { get; set; }
        public bool? IsDelete { get; set; }
    }
}
