using Hiywin.Entities.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Models.Frame
{
    public class SysGroupUserModel: ISysGroupUserModel
    {
        public int Id { get; set; }
        public string GroupNo { get; set; }
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public bool GroupMaster { get; set; }
        public bool GroupManager { get; set; }
        public string Creator { get; set; }
        public string CreateName { get; set; }
        public DateTime CreateTime { get; set; }
    }
}
