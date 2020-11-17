using Hiywin.Entities.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.IFrameService.Structs
{
    public class SysUserRoleSaveOrUpdateQuery
    {
        public string UserNo { get; set; }
        public string AppNo { get; set; }
        public List<ISysUserRoleModel> LstUserRole { get; set; }
    }
}
