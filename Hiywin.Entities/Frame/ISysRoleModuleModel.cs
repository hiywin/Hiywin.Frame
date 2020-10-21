using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Entities.Frame
{
    public interface ISysRoleModuleModel
    {
        int Id { get; set; }
        string RoleNo { get; set; }
        string ModuleNo { get; set; }
        string ModuleName { get; set; }
        string Creator { get; set; }
        string CreateName { get; set; }
        DateTime CreateTime { get; set; }

        List<ISysRolePowerModel> RolePowers { get; set; }
    }
}
