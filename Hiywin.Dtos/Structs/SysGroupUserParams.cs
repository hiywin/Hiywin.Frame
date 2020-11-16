using Hiywin.Dtos.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Dtos.Structs
{
    public class SysGroupUserParams
    {
        public string GroupNo { get; set; }
        public string CompanyNo { get; set; }
        public List<SysGroupUserDto> LstGroupUser { get; set; }
    }
}
