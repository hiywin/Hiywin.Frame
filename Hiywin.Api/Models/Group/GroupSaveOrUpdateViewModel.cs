using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Group
{
    public class GroupSaveOrUpdateViewModel
    {
        public string GroupNo { get; set; }
        [Required]
        public string GroupName { get; set; }
        public string Code { get; set; }
        public string Descr { get; set; }
        public string ParentNo { get; set; }
        [Required]
        public string AppNo { get; set; }
        public bool Access { get; set; }
        public bool IsDelete { get; set; }
    }
}
