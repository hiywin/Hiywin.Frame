using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Entities.Frame
{
    public interface ISysUserRoleModel
    {
        int Id { get; set; }
        string UserNo { get; set; }
        string RoleNo { get; set; }
        string RoleName { get; set; }
        string Creator { get; set; }
        string CreateName { get; set; }
        DateTime CreateTime { get; set; }
    }
}
