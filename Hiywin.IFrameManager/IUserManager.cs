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
    public interface IUserManager
    {
        /// <summary>
        /// 获取全部用户列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysUserModel>> GetUserAllAsync(QueryData<SysUserQuery> query);

        /// <summary>
        /// 分页获取用户列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysUserModel>> GetUserPageAsync(QueryData<SysUserQuery> query);

        /// <summary>
        /// 新增或更新用户信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> UserSaveOrUpdateAsync(QueryData<SysUserSaveOrUpdateQuery> query);

        /// <summary>
        /// 删除用户信息（IsDelete=true为软删除，IsDelete=false为物理删除）
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> UserDeleteAsync(QueryData<SysUserDeleteQuery> query);

        /// <summary>
        /// 获取所有用户角色列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysUserRoleModel>> GetUserRolesAllAsync(QueryData<SysUserRoleQuery> query);

        /// <summary>
        /// 更新用户角色
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<ErrData<bool>> UserRoleSaveOrUpdateAsync(QueryData<SysUserRoleParams> param);

        /// <summary>
        /// 删除用户角色
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> UserRoleDeleteAsync(QueryData<SysUserRoleDeleteQuery> query);
    }
}
