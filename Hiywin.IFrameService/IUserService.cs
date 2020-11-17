using Hiywin.Common.Data;
using Hiywin.Entities.Frame;
using Hiywin.IFrameService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiywin.IFrameService
{
    public interface IUserService
    {
        /// <summary>
        /// 获取全部用户列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysUserModel>>> GetUserAllAsync(QueryData<SysUserQuery> query);

        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysUserModel>>> GetUserPageAsync(QueryData<SysUserQuery> query);

        /// <summary>
        /// 新增或修改用户信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> UserSaveOrUpdateAsync(QueryData<SysUserSaveOrUpdateQuery> query);

        /// <summary>
        /// 删除用户信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> UserDeleteAsync(QueryData<SysUserDeleteQuery> query);

        /// <summary>
        /// 获取所有用户角色列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysUserRoleModel>>> GetUserRolesAllAsync(QueryData<SysUserRoleQuery> query);

        /// <summary>
        /// 用户角色保存与更新
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> UserRoleSaveOrUpdateAsync(QueryData<SysUserRoleSaveOrUpdateQuery> query);

        /// <summary>
        /// 删除用户角色
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> UserRoleDeleteAsync(QueryData<SysUserRoleDeleteQuery> query);
    }
}
