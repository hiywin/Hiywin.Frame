﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Group
{
    public class GroupUserDeleteViewModel
    {
        [Required]
        public string GroupNo { get; set; }
        [Required]
        public string UserNo { get; set; }
    }
}
