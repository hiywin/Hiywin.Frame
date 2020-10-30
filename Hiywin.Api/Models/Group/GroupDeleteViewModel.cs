using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Group
{
    public class GroupDeleteViewModel
    {
        [Required]
        public string GroupNo { get; set; }
        [Required]
        public bool IsDelete { get; set; }
    }
}
