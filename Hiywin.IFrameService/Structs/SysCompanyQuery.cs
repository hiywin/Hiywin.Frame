using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.IFrameService.Structs
{
    public class SysCompanyQuery
    {
        public string CompanyNo { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Industry { get; set; }
        public string LegalPerson { get; set; }
        public bool? IsDelete { get; set; }
    }
}
