using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Position
{
    public class PositionSaveOrUpdateViewModel
    {
        public string PositionNo { get; set; }
        [Required]
        public string PositionName { get; set; }
        [Required]
        public string CompanyNo { get; set; }
        public string Descr { get; set; }
        public bool Access { get; set; }
        public int Sort { get; set; }
        public bool IsDelete { get; set; }
    }
}
