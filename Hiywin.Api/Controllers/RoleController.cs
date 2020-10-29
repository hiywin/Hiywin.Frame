using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hiywin.Api.Models.Role;
using Hiywin.Common.Data;
using Hiywin.Common.IoC;
using Hiywin.Dtos.Structs;
using Hiywin.IFrameManager;
using Hiywin.IFrameService.Structs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Hiywin.Api.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class RoleController : BaseController
    {
        private readonly IRoleManager _manager;
        public RoleController()
        {
            _manager = IoCContainer.Resolve<IRoleManager>();
        }

        [Authorize, HttpPost, Route("get_roles_all")]
        public async Task<ActionResult> GetRolesAllAsync(GetRoleAllViewModel model)
        {
            var query = new QueryData<SysRoleQuery>()
            {
                Criteria = new SysRoleQuery()
                {
                    RoleNo = model.RoleNo,
                    RoleName = model.RoleName,
                    AppNo = model.AppNo,
                    IsDelete = model.IsDelete
                }
            };
            var result = await _manager.GetRoleAllAsync(query);

            return Ok(result);
        }

        [Authorize, HttpPost, Route("get_roles_page")]
        public async Task<ActionResult> GetRolesPageAsync(GetRolePageViewModel model)
        {
            var query = new QueryData<SysRoleQuery>()
            {
                Criteria = new SysRoleQuery()
                {
                    RoleNo = model.RoleNo,
                    RoleName = model.RoleName,
                    AppNo = model.AppNo,
                    IsDelete = model.IsDelete
                },
                PageModel = model.PageModel
            };
            var result = await _manager.GetRolePageAsync(query);

            return Ok(result);
        }

        [Authorize, HttpPost, Route("role_save_or_update")]
        public async Task<ActionResult> RoleSaveOrUpdateAsync(RoleSaveOrUpdateViewModel model)
        {
            var query = new QueryData<SysRoleSaveOrUpdateQuery>()
            {
                Criteria = new SysRoleSaveOrUpdateQuery()
                {
                    RoleNo = model.RoleNo,
                    RoleName = model.RoleName,
                    Descr = model.Descr,                    
                    AppNo = model.AppNo,
                    Access = model.Access,
                    IsDelete = model.IsDelete
                },
                Extend = new QueryExtend()
                {
                    UserNo = CurrentUser.UserNo,
                    UserName = CurrentUser.UserName
                }
            };
            var result = await _manager.RoleSaveOrUpdateAsync(query);

            return Ok(result);
        }

        [Authorize, HttpPost, Route("role_delete")]
        public async Task<ActionResult> RoleDeleteAsync(RoleDeleteViewModel model)
        {
            var query = new QueryData<SysRoleDeleteQuery>()
            {
                Criteria = new SysRoleDeleteQuery()
                {
                    RoleNo = model.RoleNo
                }
            };
            var result = await _manager.RoleDeleteAsync(query);

            return Ok(result);
        }

        [Authorize, HttpPost, Route("get_role_modules_all")]
        public async Task<ActionResult> GetRoleModulesAllAsync(GetRoleModuleAllViewModel model)
        {
            var query = new QueryData<SysRoleModuleQuery>()
            {
                Criteria = new SysRoleModuleQuery()
                {
                    RoleNo = model.RoleNo,
                    ModuleName = model.ModuleName
                }
            };
            var result = await _manager.GetRoleModuleAllAsync(query);

            return Ok(result);
        }

        [Authorize, HttpPost, Route("get_role_modules_page")]
        public async Task<ActionResult> GetRoleModulesPageAsync(GetRoleModulePageViewModel model)
        {
            var query = new QueryData<SysRoleModuleQuery>()
            {
                Criteria = new SysRoleModuleQuery()
                {
                    RoleNo = model.RoleNo,
                    ModuleName = model.ModuleName
                },
                PageModel = model.PageModel
            };
            var result = await _manager.GetRoleModulePageAsync(query);

            return Ok(result);
        }

        [Authorize, HttpPost, Route("get_role_powers_all")]
        public async Task<ActionResult> GetRolePowersAllAsync(GetRolePowerAllViewModel model)
        {
            var query = new QueryData<SysRolePowerQuery>()
            {
                Criteria = new SysRolePowerQuery()
                {
                    RoleNo = model.RoleNo,
                    PowerNo = model.PowerNo,
                    ModuleNo = model.ModuleNo
                }
            };
            var result = await _manager.GetRolePowerAllAsync(query);

            return Ok(result);
        }

        [Authorize,HttpPost,Route("role_module_save_or_update")]
        public async Task<ActionResult> RoleModuleSaveOrUpdateAsync(RoleModuleSaveOrUpdateViewModel model)
        {
            var query = new QueryData<SysRoleModuleSaveOrUpdateQuery>()
            {
                Criteria = new SysRoleModuleSaveOrUpdateQuery()
                {
                    RoleNo = model.RoleNo,
                    LstModuleNo = model.LstModuleNo
                },
                Extend = new QueryExtend()
                {
                    UserNo = CurrentUser.UserNo,
                    UserName = CurrentUser.UserName
                }
            };
            var result = await _manager.RoleModuleSaveOrUpdateAsync(query);

            return Ok(result);
        }

        [Authorize, HttpPost, Route("role_power_save_or_update")]
        public async Task<ActionResult> RolePowerSaveOrUpdateAsync(RolePowerSaveOrUpdateViewModel model)
        {
            var query = new QueryData<SysRolePowerParams>()
            {
                Criteria = new SysRolePowerParams()
                {
                    RoleNo = model.RoleNo,
                    LstRolePower = model.LstRolePower
                },
                Extend = new QueryExtend()
                {
                    UserNo = CurrentUser.UserNo,
                    UserName = CurrentUser.UserName
                }
            };
            var result = await _manager.RolePowerSaveOrUpdateAsync(query);

            return Ok(result);
        }

        [Authorize,HttpPost,Route("role_module_delete")]
        public async Task<ActionResult> RoleModuleDeleteAsync(RoleModuleDeleteViewModel model)
        {
            var query = new QueryData<SysRoleModuleDeleteQuery>()
            {
                Criteria = new SysRoleModuleDeleteQuery()
                {
                    RoleNo = model.RoleNo,
                    ModuleNo = model.ModuleNo
                }
            };
            var result = await _manager.RoleModuleDeleteAsync(query);

            return Ok(result);
        }
    }
}
