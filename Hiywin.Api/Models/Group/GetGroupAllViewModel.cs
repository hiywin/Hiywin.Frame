using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Group
{
    public class GetGroupAllViewModel
    {
        public string GroupNo { get; set; }
        public string GroupName { get; set; }
        public string Code { get; set; }
        public string ParentNo { get; set; }
        public string AppNo { get; set; }
        public bool IsDelete { get; set; }
    }
}
