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
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysModuleModel>> GetModluleAllAsync(QueryData<SysModuleQuery> query);

        /// <summary>
        /// 分页获取模块列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysModuleModel>> GetModlulePageAsync(QueryData<SysModuleQuery> query);

        /// <summary>
        /// 新增或更新模块信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> ModuleSaveOrUpdateAsync(QueryData<SysModuleSaveOrUpdateQuery> query);

        /// <summary>
        /// 删除模块信息（IsDelete=true为软删除，IsDelete=false为物理删除）
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> ModuleDeleteAsync(QueryData<SysModuleDeleteQuery> query);
    }
}
