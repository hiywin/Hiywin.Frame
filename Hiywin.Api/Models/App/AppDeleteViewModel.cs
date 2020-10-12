using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.App
{
    public class AppDeleteViewModel
    {
        [Required]
        public string AppNo { get; set; }
        [Required]
        public bool IsDelete { get; set; }
    }
}
