using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hiywin.Api.Models.Group;
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
    public class GroupController : BaseController
    {
        private readonly IGroupManager _manager;
        public GroupController()
        {
            _manager = IoCContainer.Resolve<IGroupManager>();
        }

        [Authorize, HttpPost, Route("get_groups_all")]
        public async Task<ActionResult> GetGroupsAllAsync(GetGroupAllViewModel model)
        {
            var query = new QueryData<SysGroupQuery>()
            {
                Criteria = new SysGroupQuery()
                {
                    GroupNo = model.GroupNo,
                    GroupName = model.GroupName,
                    Code = model.Code,
                    ParentNo = model.ParentNo,
                    AppNo = model.AppNo,
                    IsDelete = model.IsDelete
                }
            };
            var result = await _manager.GetGroupAllAsync(query);

            return Ok(result);
        }

        [Authorize, HttpPost, Route("get_groups_page")]
        public async Task<ActionResult> GetGroupsPageAsync(GetGroupPageViewModel model)
        {
            var query = new QueryData<SysGroupQuery>()
            {
                Criteria = new SysGroupQuery()
                {
                    GroupNo = model.GroupNo,
                    GroupName = model.GroupName,
                    Code = model.Code,
                    ParentNo = model.ParentNo,
                    AppNo = model.AppNo,
                    IsDelete = model.IsDelete
                },
                PageModel = model.PageModel
            };
            var result = await _manager.GetGroupPageAsync(query);

            return Ok(result);
        }

        [Authorize, HttpPost, Route("group_save_or_update")]
        public async Task<ActionResult> GroupSaveOrUpdateAsync(GroupSaveOrUpdateViewModel model)
        {
            var query = new QueryData<SysGroupSaveOrUpdateQuery>()
            {
                Criteria = new SysGroupSaveOrUpdateQuery()
                {
                    GroupNo = model.GroupNo,
                    GroupName = model.GroupName,
                    Code = model.Code,
                    Descr = model.Descr,
                    ParentNo = model.ParentNo,
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
            var result = await _manager.GroupSaveOrUpdateAsync(query);

            return Ok(result);
        }

        [Authorize, HttpPost, Route("group_delete")]
        public async Task<ActionResult> GroupDeleteAsync(GroupDeleteViewModel model)
        {
            var query = new QueryData<SysGroupDeleteQuery>()
            {
                Criteria = new SysGroupDeleteQuery()
                {
                    GroupNo = model.GroupNo,
                    IsDelete = model.IsDelete
                }
            };
            var result = await _manager.GroupDeleteAsync(query);

            return Ok(result);
        }

        [Authorize,HttpPost,Route("get_group_roles_all")]
        public async Task<ActionResult> GetGroupRolesAllAsync(GetGroupRolesAllViewModel model)
        {
            var query = new QueryData<SysGroupRoleQuery>()
            {
                Criteria = new SysGroupRoleQuery()
                {
                    GroupNo = model.GroupNo,
                    RoleName = model.RoleName
                }
            };
            var result = await _manager.GetGroupRolesAllAsync(query);

            return Ok(result);
        }

        [Authorize,HttpPost,Route("group_role_save_or_update")]
        public async Task<ActionResult> GroupRoleSaveOrUpdateAsync(GroupRoleSaveOrUpdateViewModel model)
        {
            var query = new QueryData<SysGroupRoleParams>()
            {
                Criteria = new SysGroupRoleParams()
                {
                    GroupNo = model.GroupNo,
                    AppNo = model.AppNo,
                    LstGroupRole = model.LstGroupRole
                },
                Extend = new QueryExtend()
                {
                    UserNo = CurrentUser.UserNo,
                    UserName = CurrentUser.UserName
                }
            };
            var result = await _manager.GroupRoleSaveOrUpdateAsync(query);

            return Ok(result);
        }

        [Authorize,HttpPost,Route("group_role_delete")]
        public async Task<ActionResult> GroupRoleDeleteAsync(GroupRoleDeleteViewModel model)
        {
            var query = new QueryData<SysGroupRoleDeleteQuery>()
            {
                Criteria = new SysGroupRoleDeleteQuery()
                {
                    GroupNo = model.GroupNo,
                    RoleNo = model.RoleNo
                }
            };
            var result = await _manager.GroupRoleDeleteAsync(query);

            return Ok(result);
        }

        [Authorize,HttpPost,Route("get_group_users_all")]
        public async Task<ActionResult> GetGroupUsersAllAsync(GetGroupUserAllViewModel model)
        {
            var query = new QueryData<SysGroupUserQuery>()
            {
                Criteria = new SysGroupUserQuery()
                {
                    GroupNo = model.GroupNo,
                    UserName = model.UserName
                }
            };
            var result = await _manager.GetGroupUsersAllAsync(query);

            return Ok(result);
        }

        [Authorize,HttpPost,Route("group_user_save_or_update")]
        public async Task<ActionResult> GroupUserSaveOrUpdateAsync(GroupUserSaveOrUpdateViewModel model)
        {
            var query = new QueryData<SysGroupUserParams>()
            {
                Criteria = new SysGroupUserParams()
                {
                    GroupNo = model.GroupNo,
                    LstGroupUser = model.LstGroupUser
                },
                Extend = new QueryExtend()
                {
                    UserNo = CurrentUser.UserNo,
                    UserName = CurrentUser.UserName
                }
            };
            var result = await _manager.GroupUserSaveOrUpdateAsync(query);

            return Ok(result);
        }

        [Authorize,HttpPost,Route("group_user_delete")]
        public async Task<ActionResult> GroupUserDeleteAsync(GroupUserDeleteViewModel model)
        {
            var query = new QueryData<SysGroupUserDeleteQuery>()
            {
                Criteria = new SysGroupUserDeleteQuery()
                {
                    GroupNo = model.GroupNo,
                    UserNo = model.UserNo
                }
            };
            var result = await _manager.GroupUserDeleteAsync(query);

            return Ok(result);
        }
    }
}
