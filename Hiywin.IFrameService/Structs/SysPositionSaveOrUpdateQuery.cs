using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.IFrameService.Structs
{
    public class SysPositionSaveOrUpdateQuery
    {
        public string PositionNo { get; set; }
        public string PositionName { get; set; }
        public string CompanyNo { get; set; }
        public string Descr { get; set; }
        public bool Access { get; set; }
        public int Sort { get; set; }
        public string Creator { get; set; }
        public string CreateName { get; set; }
        public DateTime CreateTime { get; set; }
        public string Updator { get; set; }
        public string UpdateName { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool IsDelete { get; set; }
    }
}
