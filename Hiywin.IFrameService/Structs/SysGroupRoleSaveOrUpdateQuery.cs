using Hiywin.Entities.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.IFrameService.Structs
{
    public class SysGroupRoleSaveOrUpdateQuery
    {
        public string GroupNo { get; set; }
        public string AppNo { get; set; }
        public List<ISysGroupRoleModel> LstGroupRole { get; set; }
    }
}
