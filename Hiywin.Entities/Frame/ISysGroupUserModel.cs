using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Entities.Frame
{
    public interface ISysGroupUserModel
    {
        int Id { get; set; }
        string GroupNo { get; set; }
        string UserNo { get; set; }
        bool GroupMaster { get; set; }
        bool GroupManager { get; set; }
        string Creator { get; set; }
        string CreateName { get; set; }
        DateTime CreateTime { get; set; }
        string Updator { get; set; }
        string UpdateName { get; set; }
        DateTime? UpdateTime { get; set; }
        bool IsDelete { get; set; }
    }
}
