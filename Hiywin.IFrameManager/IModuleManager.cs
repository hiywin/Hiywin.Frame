using Hiywin.Common.Data;
using Hiywin.Entities.Frame;
using System.Threading.Tasks;

namespace Hiywin.IFrameManager
{
    public interface IModuleManager
    {
        /// <summary>
        /// 获取全部模块列表
        /// </summary>
        /// <returns></returns>
        Task<ListResult<ISysModuleModel>> GetModluleAllAsync();
    }
}
