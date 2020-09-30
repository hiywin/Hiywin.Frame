using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Company
{
    public class CompanyDeleteViewModel
    {
        [Required]
        public string CompanyNo { get; set; }
        [Required]
        public bool IsDelete { get; set; }
    }
}
