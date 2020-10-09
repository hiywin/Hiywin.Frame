using Hiywin.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Dictionary
{
    public class GetDictionaryPageViewModel
    {
        public string Type { get; set; }
        public string TypeName { get; set; }
        public bool? IsDelete { get; set; }
        [Required]
        public PageModel PageModel { get; set; }
    }
}
