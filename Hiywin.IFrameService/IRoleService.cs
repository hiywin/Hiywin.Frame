using Hiywin.Common.Data;
using Hiywin.Entities.Frame;
using Hiywin.IFrameService.Structs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Hiywin.IFrameService
{
    public interface IRoleService
    {
        /// <summary>
        /// 获取所有角色列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysRoleModel>>> GetRolesAllAsync(QueryData<SysRoleQuery> query);

        /// <summary>
        /// 分页获取角色列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysRoleModel>>> GetRolesPageAsync(QueryData<SysRoleQuery> query);

        /// <summary>
        /// 新增或修改角色信息
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> RoleSaveOrUpdateAsync(QueryData<SysRoleSaveOrUpdateQuery> query);

        /// <summary>
        /// 删除角色
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> RoleDeleteAsync(QueryData<SysRoleDeleteQuery> query);

        /// <summary>
        /// 获取所有角色模块列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysRoleModuleModel>>> GetRoleModulesAllAsync(QueryData<SysRoleModuleQuery> query);

        /// <summary>
        /// 分页获取角色模块列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysRoleModuleModel>>> GetRoleModulesPageAsync(QueryData<SysRoleModuleQuery> query);

        /// <summary>
        /// 全部获取角色按钮列表
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<List<ISysRolePowerModel>>> GetRolePowersAllAsync(QueryData<SysRolePowerQuery> query);

        /// <summary>
        /// 新增或修改角色模块权限
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> RoleModuleSaveOrUpdateAsync(QueryData<SysRoleModuleSaveOrUpdateQuery> query);

        /// <summary>
        /// 新增或修改角色按钮权限
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        Task<DataResult<int>> RolePowerSaveOrUpdateAsync(QueryData<SysRolePowerSaveOrUpdateQuery> query);
    }
}
