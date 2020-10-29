using Hiywin.Common.Data;
using Hiywin.Dtos.Structs;
using Hiywin.Entities.Frame;
using Hiywin.IFrameService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiywin.IFrameManager
{
    public interface IPositionManager
    {
        /// <summary>
        /// 获取全部职位列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysPositionModel>> GetPositionAllAsync(QueryData<SysPositionQuery> query);

        /// <summary>
        /// 分页获取职位列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysPositionModel>> GetPositionPageAsync(QueryData<SysPositionQuery> query);

        /// <summary>
        /// 新增或更新职位信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> PositionSaveOrUpdateAsync(QueryData<SysPositionSaveOrUpdateQuery> query);

        /// <summary>
        /// 删除职位信息（IsDelete=true为软删除，IsDelete=false为物理删除）
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> PositionDeleteAsync(QueryData<SysPositionDeleteQuery> query);

        /// <summary>
        /// 获取全部职位所属角色列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysPositionRoleModel>> GetPositionRoleAllAsync(QueryData<SysPositionRoleQuery> query);

        /// <summary>
        /// 新增或修改职业角色
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> PositionRoleSaveOrUpdateAsync(QueryData<SysPositionRoleParams> param);

        /// <summary>
        /// 删除职位角色信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> PositionRoleDeleteAsync(QueryData<SysPositionRoleDeleteQuery> query);

    }
}
