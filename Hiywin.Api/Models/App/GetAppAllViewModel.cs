using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.App
{
    public class GetAppAllViewModel
    {
        public string AppNo { get; set; }
        public string AppName { get; set; }
        public string Leader { get; set; }
        public bool IsDelete { get; set; }
    }
}
