using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Entities.Frame
{
    public interface ISysAppModel
    {
        int Id { get; set; }
        string AppNo { get; set; }
        string AppName { get; set; }
        string Leader { get; set; }
        string Deploy { get; set; }
        string Remark { get; set; }
        string Creator { get; set; }
        string CreateName { get; set; }
        DateTime CreateTime { get; set; }
        string Updator { get; set; }
        string UpdateName { get; set; }
        DateTime? UpdateTime { get; set; }
        bool IsDelete { get; set; }
    }
}
