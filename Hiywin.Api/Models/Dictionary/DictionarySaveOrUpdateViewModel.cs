using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Dictionary
{
    public class DictionarySaveOrUpdateViewModel
    {
        public string DictionaryNo { get; set; }
        public string Type { get; set; }
        public string TypeName { get; set; }
        public string Content { get; set; }
        public string Code { get; set; }
        public string ParentNo { get; set; }
        public string Descr { get; set; }
        public string App { get; set; }
        public int Sort { get; set; }
        public bool IsDelete { get; set; }
    }
}
