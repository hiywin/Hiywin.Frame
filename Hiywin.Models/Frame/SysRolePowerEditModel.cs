using Hiywin.Entities.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Models.Frame
{
    public class SysRolePowerEditModel: ISysRolePowerEditModel
    {
        public string RoleNo { get; set; }
        public string PowerNo { get; set; }
        public string PowerName { get; set; }
        public bool IsDelete { get; set; }
        public string Creator { get; set; }
        public string CreateName { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
