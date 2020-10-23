using Hiywin.Entities.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.IFrameService.Structs
{
    public class SysRoleModuleSaveOrUpdateQuery
    {
        public string RoleNo { get; set; }
        public List<string> LstModuleNo { get; set; }
    }
}
