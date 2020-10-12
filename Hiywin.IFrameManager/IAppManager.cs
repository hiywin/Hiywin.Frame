using Hiywin.Common.Data;
using Hiywin.Entities.Frame;
using Hiywin.IFrameService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiywin.IFrameManager
{
    public interface IAppManager
    {
        /// <summary>
        /// 获取全部平台列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysAppModel>> GetAppAllAsync(QueryData<SysAppQuery> query);

        /// <summary>
        /// 分页获取平台列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysAppModel>> GetAppPageAsync(QueryData<SysAppQuery> query);

        /// <summary>
        /// 新增或更新平台信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> AppSaveOrUpdateAsync(QueryData<SysAppSaveOrUpdateQuery> query);

        /// <summary>
        /// 删除平台信息（IsDelete=true为软删除，IsDelete=false为物理删除）
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> CompanyDeleteAsync(QueryData<SysAppDeleteQuery> query);
    }
}
