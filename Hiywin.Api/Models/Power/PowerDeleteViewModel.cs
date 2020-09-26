using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Power
{
    public class PowerDeleteViewModel
    {
        [Required]
        public string PowerNo { get; set; }
        [Required]
        public bool IsDelete { get; set; }
    }
}
