using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Position
{
    public class GetPositionAllViewModel
    {
        public string PositionNo { get; set; }
        public string PositionName { get; set; }
        public string CompanyNo { get; set; }
        public bool IsDelete { get; set; }
    }
}
