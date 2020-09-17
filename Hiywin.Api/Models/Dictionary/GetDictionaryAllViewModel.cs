using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Dictionary
{
    public class GetDictionaryAllViewModel
    {
        public string Type { get; set; }
        public string TypeName { get; set; }
        public bool? IsDelete { get; set; }
    }
}
