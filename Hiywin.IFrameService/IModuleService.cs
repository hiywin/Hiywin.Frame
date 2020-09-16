using Hiywin.Common.Data;
using Hiywin.Entities.Frame;
using Hiywin.IFrameService.Structs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Hiywin.IFrameService
{
    public interface IModuleService
    {
        /// <summary>
        /// 获取所有模块列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysModuleModel>>> GetModulesAllAsync(QueryData<SysModuleQuery> query);

        /// <summary>
        /// 分页获取模块列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysModuleModel>>> GetModulesPageAsync(QueryData<SysModuleQuery> query);

        /// <summary>
        /// 新增或更新模块
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> ModuleSaveOrUpdateAsync(QueryData<SysModuleSaveOrUpdateQuery> query);

        /// <summary>
        /// 删除模块
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> ModuleDeleteAsync(QueryData<SysModuleDeleteQuery> query);
    }
}
