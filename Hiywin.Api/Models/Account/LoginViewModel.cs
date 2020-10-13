using Hiywin.Common.Enum;
using System.ComponentModel.DataAnnotations;

namespace Hiywin.Api.Models.Account
{
    public class LoginViewModel
    {
        public string UserName { get; set; }
        public string StaffNo { get; set; }
        public string AdAccount { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
        [Required]
        public string AppNo { get; set; }
    }
}
