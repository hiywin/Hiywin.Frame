using Hiywin.Entities.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Models.Frame
{
    public class SysRoleModuleModel: ISysRoleModuleModel
    {
        public SysRoleModuleModel()
        {
            RolePowers = new List<ISysRolePowerModel>();
        }
        public int Id { get; set; }
        public string RoleNo { get; set; }
        public string ModuleNo { get; set; }
        public string ModuleName { get; set; }
        public string Creator { get; set; }
        public string CreateName { get; set; }
        public DateTime CreateTime { get; set; }
        public List<ISysRolePowerModel> RolePowers { get; set; }
    }
}
