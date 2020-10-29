using Hiywin.Dtos.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Dtos.Structs
{
    public class SysRolePowerParams
    {
        public string RoleNo { get; set; }
        public List<SysRolePowerDto> LstRolePower { get; set; }
    }
}
