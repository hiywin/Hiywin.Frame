using Hiywin.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.App
{
    public class GetAppPageViewModel
    {
        public string AppNo { get; set; }
        public string AppName { get; set; }
        public string Leader { get; set; }
        public bool IsDelete { get; set; }
        [Required]
        public PageModel PageModel { get; set; }
    }
}
