using Hiywin.Entities.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Models.Frame
{
    public class SysDictionaryModel: ISysDictionaryModel
    {
        public int Id { get; set; }
        public string DictionaryNo { get; set; }
        public string Type { get; set; }
        public string TypeName { get; set; }
        public string Content { get; set; }
        public string Code { get; set; }
        public string ParentNo { get; set; }
        public string Descr { get; set; }
        public string App { get; set; }
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
