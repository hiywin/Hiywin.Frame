using Hiywin.Common.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Group
{
    public class GetGroupPageViewModel
    {
        public string GroupNo { get; set; }
        public string GroupName { get; set; }
        public string Code { get; set; }
        public string ParentNo { get; set; }
        public string AppNo { get; set; }
        public bool IsDelete { get; set; }
        [Required]
        public PageModel PageModel { get; set; }
    }
}
