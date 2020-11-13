using Hiywin.Common.Data;
using Hiywin.Entities.Frame;
using Hiywin.IFrameService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiywin.IFrameService
{
    public interface IGroupService
    {
        /// <summary>
        /// 获取所有组织列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysGroupModel>>> GetGroupsAllAsync(QueryData<SysGroupQuery> query);

        /// <summary>
        /// 分页获取组织列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysGroupModel>>> GetGroupsPageAsync(QueryData<SysGroupQuery> query);

        /// <summary>
        /// 新增或更新组织信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> GroupSaveOrUpdateAsync(QueryData<SysGroupSaveOrUpdateQuery> query);

        /// <summary>
        /// 删除组织信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> GroupDeleteAsync(QueryData<SysGroupDeleteQuery> query);

        /// <summary>
        /// 获取所有组织所属角色列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysGroupRoleModel>>> GetGroupRolesAllAsync(QueryData<SysGroupRoleQuery> query);

        /// <summary>
        /// 更新组织角色
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> GroupRoleSaveOrUpdateAsync(QueryData<SysGroupRoleSaveOrUpdateQuery> query);

        /// <summary>
        /// 删除组织角色信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> GroupRoleDeleteAsync(QueryData<SysGroupRoleDeleteQuery> query);

        /// <summary>
        /// 获取所有组织所属用户列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysGroupUserModel>>> GetGroupUsersAllAsync(QueryData<SysGroupUserQuery> query);

        /// <summary>
        /// 更新组织用户
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> GroupUserSaveOrUpdateAsync(QueryData<SysGroupUserSaveOrUpdateQuery> query);

        /// <summary>
        /// 删除组织用户信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> GroupUserDeleteAsync(QueryData<SysGroupUserDeleteQuery> query);
    }
}
