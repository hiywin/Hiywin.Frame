using Hiywin.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Company
{
    public class GetCompanyPageViewModel
    {
        public string CompanyNo { get; set; }
        public string CompanyName { get; set; }
        public string Address { get; set; }
        public string Mobile { get; set; }
        public string Industry { get; set; }
        public string LegalPerson { get; set; }
        public bool? IsDelete { get; set; }
        [Required]
        public PageModel PageModel { get; set; }
    }
}
