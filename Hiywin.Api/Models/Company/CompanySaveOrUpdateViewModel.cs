using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Company
{
    public class CompanySaveOrUpdateViewModel
    {
        public string CompanyNo { get; set; }
        [Required]
        public string CompanyName { get; set; }
        public string Abbreviation { get; set; }
        public string Address { get; set; }
        public string Industry { get; set; }
        public string LegalPerson { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public bool Access { get; set; }
        public bool IsDelete { get; set; }
    }
}
