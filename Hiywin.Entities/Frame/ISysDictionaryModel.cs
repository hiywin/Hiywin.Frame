using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Entities.Frame
{
    public interface ISysDictionaryModel
    {
        int Id { get; set; }
        string DictionaryNo { get; set; }
        string Type { get; set; }
        string TypeName { get; set; }
        string Content { get; set; }
        string Code { get; set; }
        string ParentNo { get; set; }
        string Descr { get; set; }
        string CompanyNo { get; set; }
        string Creator { get; set; }
        string CreateName { get; set; }
        DateTime CreateTime { get; set; }
        string Updator { get; set; }
        string UpdateName { get; set; }
        DateTime? UpdateTime { get; set; }
        bool IsDelete { get; set; }
    }
}