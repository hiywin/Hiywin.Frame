using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Entities.Frame
{
    public interface ISysRolePowerEditModel
    {
        string RoleNo { get; set; }
        string PowerNo { get; set; }
        string PowerName { get; set; }
        bool IsDelete { get; set; }
        string Creator { get; set; }
        string CreateName { get; set; }
        DateTime CreateTime { get; set; }
    }
}
