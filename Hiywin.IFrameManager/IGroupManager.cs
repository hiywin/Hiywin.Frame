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
    public interface IGroupManager
    {
        /// <summary>
        /// 获取全部组织列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysGroupModel>> GetGroupAllAsync(QueryData<SysGroupQuery> query);

        /// <summary>
        /// 分页获取组织列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysGroupModel>> GetGroupPageAsync(QueryData<SysGroupQuery> query);

        /// <summary>
        /// 新增或更新组织信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> GroupSaveOrUpdateAsync(QueryData<SysGroupSaveOrUpdateQuery> query);

        /// <summary>
        /// 删除组织信息（IsDelete=true为软删除，IsDelete=false为物理删除）
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> GroupDeleteAsync(QueryData<SysGroupDeleteQuery> query);

        /// <summary>
        /// 获取所有组织所属角色列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysGroupRoleModel>> GetGroupRolesAllAsync(QueryData<SysGroupRoleQuery> query);

        /// <summary>
        /// 更新组织角色
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> GroupRoleSaveOrUpdateAsync(QueryData<SysGroupRoleParams> param);

        /// <summary>
        /// 删除组织角色信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> GroupRoleDeleteAsync(QueryData<SysGroupRoleDeleteQuery> query);

        /// <summary>
        /// 获取所有组织所属用户列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysGroupUserModel>> GetGroupUsersAllAsync(QueryData<SysGroupUserQuery> query);

        /// <summary>
        /// 更新组织用户
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> GroupUserSaveOrUpdateAsync(QueryData<SysGroupUserParams> param);

        /// <summary>
        /// 删除组织用户信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> GroupUserDeleteAsync(QueryData<SysGroupUserDeleteQuery> query);
    }
}
