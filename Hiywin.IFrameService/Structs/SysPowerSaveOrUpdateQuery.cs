using Hiywin.Entities.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.IFrameService.Structs
{
    public class SysPowerSaveOrUpdateQuery
    {
        public string PowerNo { get; set; }
        public string ModuleNo { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string Style { get; set; }
        public string FuncName { get; set; }
        public string Icon { get; set; }
        public int Sort { get; set; }
        public bool Access { get; set; }
        public string Creator { get; set; }
        public string CreateName { get; set; }
        public DateTime CreateTime { get; set; }
        public string Updator { get; set; }
        public string UpdateName { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool IsDelete { get; set; }
        public bool IsPlain { get; set; }
        public bool IsRound { get; set; }
        public bool IsCircle { get; set; }
    }
}
