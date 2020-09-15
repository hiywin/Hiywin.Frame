using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Entities.Frame
{
    public interface ISysUserModel
    {
        int Id { get; set; }
        string UserNo { get; set; }
        string UserName { get; set; }
        string Pwd { get; set; }
        string Mobile { get; set; }
        string Email { get; set; }
        string Icon { get; set; }
        int App { get; set; }
        DateTime RegisterTime { get; set; }
        string ApprovedBy { get; set; }
        DateTime? ApprovedTime { get; set; }
        string Descr { get; set; }
        string RejectedBy { get; set; }
        DateTime? RejectedTime { get; set; }
        string RejectedReason { get; set; }
        bool Access { get; set; }
        string Creator { get; set; }
        string CreateName { get; set; }
        DateTime? CreateTime { get; set; }
        string Updator { get; set; }
        string UpdateName { get; set; }
        DateTime? UpdateTime { get; set; }
        string IsDelete { get; set; }
        string StaffNo { get; set; }
        string CompanyNo { get; set; }
        bool IsAdmin { get; set; }
        string AdAccount { get; set; }
    }
}
