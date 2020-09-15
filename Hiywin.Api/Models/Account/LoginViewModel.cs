using Hiywin.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace Hiywin.Api.Models.Account
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string StaffNo { get; set; }
        public string AdAccount { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public AppEnum App { get; set; }
    }
}
