using Hiywin.Entities.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.IFrameService.Structs
{
    public class SysPositionRoleSaveOrUpdateQuery
    {
        public string PositionNo { get; set; }
        public string AppNo { get; set; }
        public List<ISysPositionRoleModel> LstPositionRole { get; set; }
    }
}
