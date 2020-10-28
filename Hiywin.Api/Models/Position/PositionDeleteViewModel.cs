using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Position
{
    public class PositionDeleteViewModel
    {
        [Required]
        public string PositionNo { get; set; }
        [Required]
        public bool IsDelete { get; set; }
    }
}
