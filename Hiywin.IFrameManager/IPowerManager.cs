using Hiywin.Common.Data;
using Hiywin.Entities.Frame;
using Hiywin.IFrameService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiywin.IFrameManager
{
    public interface IPowerManager
    {
        /// <summary>
        /// 获取全部按钮列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysPowerModel>> GetPowerAllAsync(QueryData<SysPowerQuery> query);

        /// <summary>
        /// 分页获取按钮列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysPowerModel>> GetPowerPageAsync(QueryData<SysPowerQuery> query);

        /// <summary>
        /// 新增或更新按钮信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> PowerSaveOrUpdateAsync(QueryData<SysPowerSaveOrUpdateQuery> query);

        /// <summary>
        /// 删除按钮信息（IsDelete=true为软删除，IsDelete=false为物理删除）
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> PowerDeleteAsync(QueryData<SysPowerDeleteQuery> query);
    }
}
