using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.App
{
    public class AppSaveOrUpdateViewModel
    {
        public string AppNo { get; set; }
        [Required]
        public string AppName { get; set; }
        [Required]
        public string Leader { get; set; }
        public string Deploy { get; set; }
        public string Remark { get; set; }
        public bool IsDelete { get; set; }
    }
}
