using Hiywin.Dtos.Frame;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hiywin.Api.Models.Group
{
    public class GroupUserSaveOrUpdateViewModel
    {
        [Required]
        public string GroupNo { get; set; }
        [Required]
        public string CompanyNo { get; set; }
        [Required]
        public List<SysGroupUserDto> LstGroupUser { get; set; }
    }
}
