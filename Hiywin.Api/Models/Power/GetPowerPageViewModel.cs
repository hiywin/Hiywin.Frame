using Hiywin.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Power
{
    public class GetPowerPageViewModel
    {
        public string ModudelNo { get; set; }
        public string PowerNo { get; set; }
        public bool IsDelete { get; set; }
        [Required]
        public PageModel PageModel { get; set; }
    }
}
