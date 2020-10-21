using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Entities.Frame
{
    public interface ISysRolePowerModel
    {
        int Id { get; set; }
        string RoleNo { get; set; }
        string PowerNo { get; set; }
        string ModuleNo { get; set; }
        string Content { get; set; }
        string Creator { get; set; }
        string CreateName { get; set; }
        DateTime CreateTime { get; set; }
    }
}
