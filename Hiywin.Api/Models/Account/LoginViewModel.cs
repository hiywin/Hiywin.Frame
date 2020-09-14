using Hiywin.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace Hiywin.Api.Models.Account
{
    public class LoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public AppEnum App { get; set; }
    }
}
