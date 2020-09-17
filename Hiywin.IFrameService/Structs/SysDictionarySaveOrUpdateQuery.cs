using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.IFrameService.Structs
{
    public class SysDictionarySaveOrUpdateQuery
    {
        public string DictionaryNo { get; set; }
        public string Type { get; set; }
        public string TypeName { get; set; }
        public string Content { get; set; }
        public string Code { get; set; }
        public string ParentNo { get; set; }
        public string Descr { get; set; }
        public string CompanyNo { get; set; }
        public bool IsDelete { get; set; }
        public string Creator { get; set; }
        public string CreateName { get; set; }
        public DateTime CreateTime { get; set; }
        public string Updator { get; set; }
        public string UpdateName { get; set; }
        public DateTime? UpdateTime { get; set; }
    }
}
