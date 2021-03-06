﻿using Hiywin.Common.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Account
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        public string RealName { get; set; }
        public string StaffNo { get; set; }
        public string AdAccount { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        [Required]
        public string AppNo { get; set; }
    }
}
