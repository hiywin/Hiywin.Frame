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
        string StaffNo { get; set; }
        string AdAccount { get; set; }
        string Mobile { get; set; }
        string Email { get; set; }
        string Pwd { get; set; }
        string RealName { get; set; }
        bool IsAdmin { get; set; }
        string Icon { get; set; }
        string AppNo { get; set; }
        string CompanyNo { get; set; }
        DateTime RegisterTime { get; set; }
        string ApprovedBy { get; set; }
        string ApprovedName { get; set; }
        DateTime? ApprovedTime { get; set; }
        string Descr { get; set; }
        string RejectedBy { get; set; }
        string RejectedName { get; set; }
        DateTime? RejectedTime { get; set; }
        string RejectedReason { get; set; }
        bool Access { get; set; }
        string Creator { get; set; }
        string CreateName { get; set; }
        DateTime? CreateTime { get; set; }
        string Updator { get; set; }
        string UpdateName { get; set; }
        DateTime? UpdateTime { get; set; }
        bool IsDelete { get; set; }
        List<ISysUserRoleModel> LstUserRole { get; set; }
    }
}
