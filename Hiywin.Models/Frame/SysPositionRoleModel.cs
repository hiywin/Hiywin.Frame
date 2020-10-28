using Hiywin.Entities.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Models.Frame
{
    public class SysPositionRoleModel: ISysPositionRoleModel
    {
        public int Id { get; set; }
        public string PositionNo { get; set; }
        public string RoleNo { get; set; }
        public string RoleName { get; set; }
        public string Creator { get; set; }
        public string CreateName { get; set; }
        public string CreateTime { get; set; }
    }
}
