using Hiywin.Entities.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.IFrameService.Structs
{
    public class SysRolePowerSaveOrUpdateQuery
    {
        public string RoleNo { get; set; }
        public List<ISysRolePowerEditModel> LstRolePower { get; set; }
    }
}
