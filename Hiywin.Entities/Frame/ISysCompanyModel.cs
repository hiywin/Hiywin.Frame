using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Entities.Frame
{
    public interface ISysCompanyModel
    {
        int Id { get; set; }
        string CompanyNo { get; set; }
        string CompanyName { get; set; }
        string Abbreviation { get; set; }
        string Address { get; set; }
        string Industry { get; set; }
        string LegalPerson { get; set; }
        string Contact { get; set; }
        string Phone { get; set; }
        string Mobile { get; set; }
        string Email { get; set; }
        string Website { get; set; }
        bool Access { get; set; }
        string Creator { get; set; }
        string CreateName { get; set; }
        DateTime CreateTime { get; set; }
        string Updator { get; set; }
        string UpdateName { get; set; }
        DateTime? UpdateTime { get; set; }
        bool IsDelete { get; set; }
    }
}
