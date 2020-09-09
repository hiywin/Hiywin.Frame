using Hiywin.Common.IoC;
using Hiywin.Entities.Frame;
using Hiywin.IFrameService;
using Hiywin.IFrameService.Structs;
using System;

namespace Hiywin.FrameService
{
    public class ModuleService : IModuleService
    {
        public ISysModuleModel GetModules(ModuleQuery moduleModel)
        {
            Console.WriteLine("ModuleService：" + moduleModel.Name);

            ISysModuleModel model = IoCContainer.Resolve<ISysModuleModel>();
            model.Id = 1;
            model.Name = moduleModel.Name;
            model.Age = 20;

            return model;
        }
    }
}
