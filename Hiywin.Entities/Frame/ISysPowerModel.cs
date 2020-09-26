using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Entities.Frame
{
    public interface ISysPowerModel
    {
        int Id { get; set; }
        string PowerNo { get; set; }
        string ModuleNo { get; set; }
        string Content { get; set; }
        string Type { get; set; }
        string Style { get; set; }
        string FuncName { get; set; }
        string Icon { get; set; }
        int Sort { get; set; }
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
