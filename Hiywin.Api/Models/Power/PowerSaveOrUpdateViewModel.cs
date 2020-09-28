using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Power
{
    public class PowerSaveOrUpdateViewModel
    {
        public string PowerNo { get; set; }
        [Required]
        public string ModuleNo { get; set; }
        public string Content { get; set; }
        public string Type { get; set; }
        public string Style { get; set; }
        public string FuncName { get; set; }
        public string Icon { get; set; }
        public int Sort { get; set; }
        [Required]
        public bool Access { get; set; }
        [Required]
        public bool IsDelete { get; set; }
        public bool IsPlain { get; set; }
        public bool IsRound { get; set; }
        public bool IsCircle { get; set; }
    }
}
