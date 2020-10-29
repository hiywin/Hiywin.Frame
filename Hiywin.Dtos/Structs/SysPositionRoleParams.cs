using Hiywin.Dtos.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Dtos.Structs
{
    public class SysPositionRoleParams
    {
        public string PositionNo { get; set; }
        public string AppNo { get; set; }
        public List<SysPositionRoleDto> LstPositionRole { get; set; }
    }
}
