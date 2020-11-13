using Hiywin.Entities.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.IFrameService.Structs
{
    public class SysGroupUserSaveOrUpdateQuery
    {
        public string GroupNo { get; set; }
        public List<ISysGroupUserModel> LstGroupUser { get; set; }
    }
}
