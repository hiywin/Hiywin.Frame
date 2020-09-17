using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Dictionary
{
    public class DictionaryDeleteViewModel
    {
        [Required]
        public string DictionaryNo { get; set; }
        [Required]
        public bool IsDelete { get; set; }
    }
}
