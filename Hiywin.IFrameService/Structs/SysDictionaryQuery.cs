using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.IFrameService.Structs
{
    public class SysDictionaryQuery
    {
        public string Type { get; set; }
        public string TypeName { get; set; }
        public bool? IsDelete { get; set; }
    }
}
