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
    public interface IRoleManager
    {
        /// <summary>
        /// 获取全部角色列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysRoleModel>> GetRoleAllAsync(QueryData<SysRoleQuery> query);

        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysRoleModel>> GetRolePageAsync(QueryData<SysRoleQuery> query);

        /// <summary>
        /// 新增或更新角色信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> RoleSaveOrUpdateAsync(QueryData<SysRoleSaveOrUpdateQuery> query);

        /// <summary>
        /// 删除角色（IsDelete=true为软删除，IsDelete=false为物理删除）
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> RoleDeleteAsync(QueryData<SysRoleDeleteQuery> query);

        /// <summary>
        ///获取全部角色模块列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysRoleModuleModel>> GetRoleModuleAllAsync(QueryData<SysRoleModuleQuery> query);

        /// <summary>
        /// 分页获取角色模块列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysRoleModuleModel>> GetRoleModulePageAsync(QueryData<SysRoleModuleQuery> query);

        /// <summary>
        /// 全部获取角色按钮列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ListResult<ISysRolePowerModel>> GetRolePowerAllAsync(QueryData<SysRolePowerQuery> query);

        /// <summary>
        /// 新增或修改角色模块权限
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> RoleModuleSaveOrUpdateAsync(QueryData<SysRoleModuleSaveOrUpdateQuery> query);

        /// <summary>
        /// 新增或修改角色按钮权限
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> RolePowerSaveOrUpdateAsync(QueryData<SysRolePowerParam> query);

        /// <summary>
        /// 删除角色模块权限及所属按钮权限
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<ErrData<bool>> RoleModuleDeleteAsync(QueryData<SysRoleModuleDeleteQuery> query);
    }
}
