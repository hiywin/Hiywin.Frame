using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hiywin.Api.Models.User;
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
    public class UserController : BaseController
    {
        private readonly IUserManager _manager;
        public UserController()
        {
            _manager = IoCContainer.Resolve<IUserManager>();
        }

        [Authorize,HttpPost,Route("get_users_all")]
        public async Task<ActionResult> GetUsersAllAsync(GetUserAllViewModel model)
        {
            var query = new QueryData<SysUserQuery>()
            {
                Criteria = new SysUserQuery()
                {
                    UserNo = model.UserNo,
                    UserName = model.UserName,
                    StaffNo = model.StaffNo,
                    AdAccount = model.AdAccount,
                    Mobile = model.Mobile,
                    Email = model.Email,
                    RealName = model.RealName,
                    CompanyNo = model.CompanyNo,
                    Access = model.Access,
                    IsDelete = model.IsDelete
                }
            };
            var result = await _manager.GetUserAllAsync(query);

            return Ok(result);
        }

        [Authorize,HttpPost,Route("get_users_page")]
        public async Task<ActionResult> GetUsersPageAsync(GetUserPageViewModel model)
        {
            var query = new QueryData<SysUserQuery>()
            {
                Criteria = new SysUserQuery()
                {
                    UserNo = model.UserNo,
                    UserName = model.UserName,
                    StaffNo = model.StaffNo,
                    AdAccount = model.AdAccount,
                    Mobile = model.Mobile,
                    Email = model.Email,
                    RealName = model.RealName,
                    ApprovedName = model.ApprovedName,
                    RejectedName = model.RejectedName,
                    CompanyNo = model.CompanyNo,
                    Access = model.Access,
                    IsDelete = model.IsDelete
                },
                PageModel = model.PageModel,
                Extend = new QueryExtend()
                {
                    IsAdmin = CurrentUser.IsAdmin
                }
            };
            var result = await _manager.GetUserPageAsync(query);

            return Ok(result);
        }

        [Authorize,HttpPost,Route("user_save_or_update")]
        public async Task<ActionResult> UserSaveOrUpdateAsync(UserSaveOrUpdateViewModel model)
        {
            var query = new QueryData<SysUserSaveOrUpdateQuery>()
            {
                Criteria = new SysUserSaveOrUpdateQuery()
                {
                    UserNo = model.UserNo,
                    UserName = model.UserName,
                    StaffNo = model.StaffNo,
                    AdAccount = model.AdAccount,
                    Mobile = model.Mobile,
                    Email = model.Email,
                    Pwd = model.Pwd,
                    RealName = model.RealName,
                    IsAdmin = model.IsAdmin,
                    Icon = model.Icon,
                    AppNo = model.AppNo,
                    CompanyNo = model.CompanyNo,
                    ApprovedBy = model.ApprovedBy,
                    ApprovedName = model.ApprovedName,
                    Descr = model.Descr,
                    RejectedBy = model.RejectedBy,
                    RejectedName = model.RejectedName,
                    RejectedReason = model.RejectedReason,
                    Access = model.Access,
                    IsDelete = model.IsDelete
                },
                Extend = new QueryExtend()
                {
                    UserNo = CurrentUser.UserNo,
                    UserName = CurrentUser.UserName,
                    IsAdmin = CurrentUser.IsAdmin
                }
            };
            var result = await _manager.UserSaveOrUpdateAsync(query);

            return Ok(result);
        }

        [Authorize,HttpPost,Route("user_delete")]
        public async Task<ActionResult> UserDeleteAsync(UserDeleteViewModel model)
        {
            var query = new QueryData<SysUserDeleteQuery>()
            {
                Criteria = new SysUserDeleteQuery()
                {
                    UserNo = model.UserNo,
                    IsDelete = model.IsDelete
                }
            };
            var result = await _manager.UserDeleteAsync(query);

            return Ok(result);
        }
    }
}
