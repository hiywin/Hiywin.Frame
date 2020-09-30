using Hiywin.Entities.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Models.Frame
{
    public class SysCompanyModel: ISysCompanyModel
    {
        public int Id { get; set; }
        public string CompanyNo { get; set; }
        public string CompanyName { get; set; }
        public string Abbreviation { get; set; }
        public string Address { get; set; }
        public string Industry { get; set; }
        public string LegalPerson { get; set; }
        public string Contact { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public bool Access { get; set; }
        public string Creator { get; set; }
        public string CreateName { get; set; }
        public DateTime CreateTime { get; set; }
        public string Updator { get; set; }
        public string UpdateName { get; set; }
        public DateTime? UpdateTime { get; set; }
        public bool IsDelete { get; set; }
    }
}
