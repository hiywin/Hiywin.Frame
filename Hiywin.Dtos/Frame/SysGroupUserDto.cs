using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Dtos.Frame
{
    public class SysGroupUserDto
    {
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public bool GroupMaster { get; set; }
        public bool GroupManager { get; set; }
    }
}
