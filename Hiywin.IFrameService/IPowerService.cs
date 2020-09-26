using Hiywin.Common.Data;
using Hiywin.Entities.Frame;
using Hiywin.IFrameService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiywin.IFrameService
{
    public interface IPowerService
    {
        /// <summary>
        /// 获取所有按钮列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysPowerModel>>> GetPowersAllAsync(QueryData<SysPowerQuery> query);

        /// <summary>
        /// 分页获取按钮列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysPowerModel>>> GetPowersPageAsync(QueryData<SysPowerQuery> query);

        /// <summary>
        /// 新增或更新按钮信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> PowerSaveOrUpdateAsync(QueryData<SysPowerSaveOrUpdateQuery> query);

        /// <summary>
        /// 删除按钮
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> PowerDeleteAsync(QueryData<SysPowerDeleteQuery> query);
    }
}
