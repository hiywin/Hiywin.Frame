using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Common.Jwt
{
    public class LoginUser
    {
        public string UserNo { get; set; }
        public string UserName { get; set; }
        public string AdAccount { get; set; }
        public bool IsAdmin { get; set; }
        public string StaffNo { get; set; }
    }
}
