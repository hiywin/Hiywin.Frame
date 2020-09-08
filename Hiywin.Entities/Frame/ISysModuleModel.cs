using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.Entities.Frame
{
    public interface ISysModuleModel
    {
        int Id { get; set; }
        string Name { get; set; }
        string Code { get; set; }
        int Age { get; set; }
    }
}
