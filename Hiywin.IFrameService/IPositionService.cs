using Hiywin.Common.Data;
using Hiywin.Entities.Frame;
using Hiywin.IFrameService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiywin.IFrameService
{
    public interface IPositionService
    {
        /// <summary>
        /// 获取所有职位列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysPositionModel>>> GetPositionsAllAsync(QueryData<SysPositionQuery> query);

        /// <summary>
        /// 分页获取职位列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysPositionModel>>> GetPositionsPageAsync(QueryData<SysPositionQuery> query);

        /// <summary>
        /// 新增或更新职位信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> PositionSaveOrUpdateAsync(QueryData<SysPositionSaveOrUpdateQuery> query);

        /// <summary>
        /// 删除职位信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> PositionDeleteAsync(QueryData<SysPositionDeleteQuery> query);

        /// <summary>
        /// 获取所有职位所属角色列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysPositionRoleModel>>> GetPositionRolesAllAsync(QueryData<SysPositionRoleQuery> query);

        /// <summary>
        /// 删除职位角色信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> PositionRoleDeleteAsync(QueryData<SysPositionRoleDeleteQuery> query);
    }
}
