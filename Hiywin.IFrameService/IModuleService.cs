using Hiywin.Entities.Frame;
using System;
using System.Collections.Generic;
using System.Text;

namespace Hiywin.IFrameService
{
    public interface IModuleService
    {
        ISysModuleModel GetModules(Structs.ModuleQuery moduleModel);
    }
}
