using Hiywin.Entities.Frame;
using Hiywin.IFrameManager;
using Hiywin.IFrameService;

namespace Hiywin.FrameManager
{
    public class ModuleManager : IModuleManager
    {
        IModuleService _service;

        public ModuleManager(IModuleService service)
        {
            _service = service;
        }
        public ISysModuleModel GetModuleManager()
        {
            var condition = new IFrameService.Structs.ModuleQuery()
            {
                Name = "James"
            };

            var result = _service.GetModules(condition);

            return result;
        }
    }
}
