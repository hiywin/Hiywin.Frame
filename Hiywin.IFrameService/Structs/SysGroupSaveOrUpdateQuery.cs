using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.IFrameService.Structs
{
    public class SysGroupSaveOrUpdateQuery
    {
        public int Id { get; set; }
        public string GroupNo { get; set; }
        public string GroupName { get; set; }
        public string Code { get; set; }
        public string Descr { get; set; }
        public string ParentNo { get; set; }
        public string AppNo { get; set; }
        public bool Access { get; set; }
        public string Creator { get; set; }
        public string CreateName { get; set; }
        public DateTime CreateTime { get; set; }
        public string Updator { get; set; }
        public string UpdateName { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool IsDelete { get; set; }
    }
}
