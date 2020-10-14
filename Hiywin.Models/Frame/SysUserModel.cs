using Hiywin.Entities.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Models.Frame
{
    public class SysUserModel: ISysUserModel
    {
        public int Id { get; set; }
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public string StaffNo { get; set; }
        public string AdAccount { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Pwd { get; set; }
        public string RealName { get; set; }
        public bool IsAdmin { get; set; }
        public string Icon { get; set; }
        public string AppNo { get; set; }
        public string CompanyNo { get; set; }
        public DateTime RegisterTime { get; set; }
        public string ApprovedBy { get; set; }
        public string ApprovedName { get; set; }
        public DateTime? ApprovedTime { get; set; }
        public string Descr { get; set; }
        public string RejectedBy { get; set; }
        public string RejectedName { get; set; }
        public DateTime? RejectedTime { get; set; }
        public string RejectedReason { get; set; }
        public bool Access { get; set; }
        public string Creator { get; set; }
        public string CreateName { get; set; }
        public DateTime? CreateTime { get; set; }
        public string Updator { get; set; }
        public string UpdateName { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool IsDelete { get; set; }
    }
}
