using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Entities.Frame
{
    public interface ISysGroupModel
    {
        int Id { get; set; }
        string GroupNo { get; set; }
        string GroupName { get; set; }
        string Code { get; set; }
        string Descr { get; set; }
        string ParentNo { get; set; }
        string AppNo { get; set; }
        bool Access { get; set; }
        string Creator { get; set; }
        string CreateName { get; set; }
        DateTime CreateTime { get; set; }
        string Updator { get; set; }
        string UpdateName { get; set; }
        DateTime? UpdateTime { get; set; }
        bool IsDelete { get; set; }
    }
}
