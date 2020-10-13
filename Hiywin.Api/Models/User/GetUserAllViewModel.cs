using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.User
{
    public class GetUserAllViewModel
    {
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public string StaffNo { get; set; }
        public string AdAccount { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string RealName { get; set; }
        public string CompanyNo { get; set; }
        public bool? Access { get; set; }
        public bool IsDelete { get; set; }
    }
}
