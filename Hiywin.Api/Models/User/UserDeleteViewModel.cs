using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.User
{
    public class UserDeleteViewModel
    {
        [Required]
        public string UserNo { get; set; }
        [Required]
        public bool IsDelete { get; set; }
    }
}
