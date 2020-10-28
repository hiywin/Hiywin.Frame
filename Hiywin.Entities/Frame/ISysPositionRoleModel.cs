using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Entities.Frame
{
    public interface ISysPositionRoleModel
    {
        int Id { get; set; }
        string PositionNo { get; set; }
        string RoleNo { get; set; }
        string RoleName { get; set; }
        string Creator { get; set; }
        string CreateName { get; set; }
        string CreateTime { get; set; }
    }
}
