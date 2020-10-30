using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hiywin.Api.Models.Group;
using Hiywin.Common.Data;
using Hiywin.Common.IoC;
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
    }
}
