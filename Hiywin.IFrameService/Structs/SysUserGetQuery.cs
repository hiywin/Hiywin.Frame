using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.IFrameService.Structs
{
    public class SysUserGetQuery
    {
        public string UserName { get; set; }
        public string Pwd { get; set; }
        public bool IsDelete { get; set; }
    }
}
