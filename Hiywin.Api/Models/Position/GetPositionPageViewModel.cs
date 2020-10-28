using Hiywin.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Position
{
    public class GetPositionPageViewModel
    {
        public string PositionNo { get; set; }
        public string PositionName { get; set; }
        public string CompanyNo { get; set; }
        public bool IsDelete { get; set; }
        [Required]
        public PageModel PageModel { get; set; }
    }
}
