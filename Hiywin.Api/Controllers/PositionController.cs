using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hiywin.Api.Models.Position;
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
    public class PositionController : BaseController
    {
        private readonly IPositionManager _manager;
        public PositionController()
        {
            _manager = IoCContainer.Resolve<IPositionManager>();
        }

        [Authorize, HttpPost, Route("get_positions_all")]
        public async Task<ActionResult> GetPositionsAllAsync(GetPositionAllViewModel model)
        {
            var query = new QueryData<SysPositionQuery>()
            {
                Criteria = new SysPositionQuery()
                {
                    PositionNo = model.PositionNo,
                    PositionName = model.PositionName,
                    CompanyNo = model.CompanyNo,
                    IsDelete = model.IsDelete
                }
            };
            var result = await _manager.GetPositionAllAsync(query);

            return Ok(result);
        }

        [Authorize, HttpPost, Route("get_positions_page")]
        public async Task<ActionResult> GetPositionsPageAsync(GetPositionPageViewModel model)
        {
            var query = new QueryData<SysPositionQuery>()
            {
                Criteria = new SysPositionQuery()
                {
                    PositionNo = model.PositionNo,
                    PositionName = model.PositionName,
                    CompanyNo = model.CompanyNo,
                    IsDelete = model.IsDelete
                },
                PageModel = model.PageModel
            };
            var result = await _manager.GetPositionPageAsync(query);

            return Ok(result);
        }

        [Authorize, HttpPost, Route("position_save_or_update")]
        public async Task<ActionResult> PositionSaveOrUpdateAsync(PositionSaveOrUpdateViewModel model)
        {
            var query = new QueryData<SysPositionSaveOrUpdateQuery>()
            {
                Criteria = new SysPositionSaveOrUpdateQuery()
                {
                    PositionNo = model.PositionNo,
                    PositionName = model.PositionName,
                    CompanyNo = model.CompanyNo,
                    Descr = model.Descr,
                    Access = model.Access,
                    Sort = model.Sort,
                    IsDelete = model.IsDelete
                },
                Extend = new QueryExtend()
                {
                    UserNo = CurrentUser.UserNo,
                    UserName = CurrentUser.UserName
                }
            };
            var result = await _manager.PositionSaveOrUpdateAsync(query);

            return Ok(result);
        }

        [Authorize, HttpPost, Route("position_delete")]
        public async Task<ActionResult> PositionDeleteAsync(PositionDeleteViewModel model)
        {
            var query = new QueryData<SysPositionDeleteQuery>()
            {
                Criteria = new SysPositionDeleteQuery()
                {
                    PositionNo = model.PositionNo,
                    IsDelete = model.IsDelete
                }
            };
            var result = await _manager.PositionDeleteAsync(query);

            return Ok(result);
        }

        [Authorize, HttpPost, Route("get_position_roles_all")]
        public async Task<ActionResult> GetPositionRolesAllAsync(GetPositionRoleAllViewModel model)
        {
            var query = new QueryData<SysPositionRoleQuery>()
            {
                Criteria = new SysPositionRoleQuery()
                {
                    PositionNo = model.PositionNo,
                    RoleName = model.RoleName
                }
            };
            var result = await _manager.GetPositionRoleAllAsync(query);

            return Ok(result);
        }

        [Authorize, HttpPost, Route("position_role_delete")]
        public async Task<ActionResult> PositionRoleDeleteAsync(PositionRoleDeleteViewModel model)
        {
            var query = new QueryData<SysPositionRoleDeleteQuery>()
            {
                Criteria = new SysPositionRoleDeleteQuery()
                {
                    PositionNo = model.PositionNo,
                    RoleNo = model.RoleNo
                }
            };
            var result = await _manager.PositionRoleDeleteAsync(query);

            return Ok(result);
        }
    }
}
