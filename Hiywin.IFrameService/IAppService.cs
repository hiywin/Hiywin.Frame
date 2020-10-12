using Hiywin.Common.Data;
using Hiywin.Entities.Frame;
using Hiywin.IFrameService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiywin.IFrameService
{
    public interface IAppService
    {
        /// <summary>
        /// 获取所有平台列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysAppModel>>> GetAppsAllAsync(QueryData<SysAppQuery> query);

        /// <summary>
        /// 分页获取平台列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysAppModel>>> GetAppsPageAsync(QueryData<SysAppQuery> query);

        /// <summary>
        /// 新增或更新平台信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> AppSaveOrUpdateAsync(QueryData<SysAppSaveOrUpdateQuery> query);

        /// <summary>
        /// 删除平台信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> AppDeleteAsync(QueryData<SysAppDeleteQuery> query);
    }
}
