using Hiywin.Common.Data;
using Hiywin.Entities.Frame;
using Hiywin.IFrameService.Structs;
using System.Threading.Tasks;

namespace Hiywin.IFrameManager
{
    public interface IModuleManager
    {
        /// <summary>
        /// 获取全部模块列表
        /// </summary>
        /// <returns></returns>
        Task<ListResult<ISysModuleModel>> GetModluleAllAsync(QueryData<SysModuleQuery> query);
    }
}
