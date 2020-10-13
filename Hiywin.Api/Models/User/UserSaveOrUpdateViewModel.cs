using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.User
{
    public class UserSaveOrUpdateViewModel
    {
        public string UserNo { get; set; }
        [Required]
        public string UserName { get; set; }
        public string StaffNo { get; set; }
        public string AdAccount { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        [Required]
        public string Pwd { get; set; }
        public string RealName { get; set; }
        public bool IsAdmin { get; set; }
        public string Icon { get; set; }
        public string AppNo { get; set; }
        public string CompanyNo { get; set; }
        public string ApprovedBy { get; set; }
        public string Descr { get; set; }
        public string RejectedBy { get; set; }
        public string RejectedReason { get; set; }
        public bool Access { get; set; }
        public bool IsDelete { get; set; }
    }
}
