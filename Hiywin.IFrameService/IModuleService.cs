using Hiywin.Common.Data;
using Hiywin.Entities.Frame;
using Hiywin.IFrameService.Structs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hiywin.IFrameService
{
    public interface IModuleService
    {
        Task<DataResult<List<ISysModuleModel>>> GetModulesAllAsync(QueryData<SysModuleQuery> query);
    }
}
