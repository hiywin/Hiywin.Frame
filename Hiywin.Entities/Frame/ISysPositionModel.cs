using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Entities.Frame
{
    public interface ISysPositionModel
    {
        int Id { get; set; }
        string PositionNo { get; set; }
        string PositionName { get; set; }
        string CompanyNo { get; set; }
        string Descr { get; set; }
        bool Access { get; set; }
        int Sort { get; set; }
        string Creator { get; set; }
        string CreateName { get; set; }
        DateTime CreateTime { get; set; }
        string Updator { get; set; }
        string UpdateName { get; set; }
        DateTime? UpdateTime { get; set; }
        bool IsDelete { get; set; }
    }
}
