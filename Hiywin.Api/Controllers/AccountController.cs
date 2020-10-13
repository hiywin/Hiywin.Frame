using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hiywin.Api.Models.Account;
using Hiywin.Common.Data;
using Hiywin.Common.IoC;
using Hiywin.Common.Jwt;
using Hiywin.IFrameManager;
using Hiywin.IFrameService.Structs;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace Hiywin.Api.Controllers
{
    [Route("v1/api/[controller]")]
    [ApiController]
    public class AccountController : BaseController
    {
        IAccountManager _manager;
        IJwtFactory _jwtFactory;
        public AccountController()
        {
            _manager = IoCContainer.Resolve<IAccountManager>();
            _jwtFactory = IoCContainer.Resolve<IJwtFactory>();
        }

        /// <summary>
        /// 启动提示
        /// </summary>
        /// <returns></returns>
        [HttpGet, Route("app_loaded")]
        public ActionResult AppLoaded()
        {
            string result = "程序启动成功...";

            return Ok(result);
        }

        /// <summary>
        /// 注册
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("register")]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            var query = new QueryData<SysUserSaveOrUpdateQuery>()
            {
                Criteria = new SysUserSaveOrUpdateQuery()
                {
                    UserName = model.UserName,
                    Pwd = model.Password,
                    RealName = model.RealName,
                    StaffNo = model.StaffNo,
                    AdAccount = model.AdAccount,
                    Mobile = model.Mobile,
                    Email = model.Email,
                    AppNo = model.AppNo
                }
            };
            var result = await _manager.RegisterAsync(query);

            return Ok(result);
        }

        /// <summary>
        /// 登录
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost, Route("login")]
        public async Task<ActionResult> Login(LoginViewModel model)
        {
            var result = new ErrData<string>();

            var query = new QueryData<SysUserQuery>()
            {
                Criteria = new SysUserQuery()
                {
                    UserName = model.UserName,
                    StaffNo = model.StaffNo,
                    AdAccount = model.AdAccount,
                    Mobile = model.Mobile,
                    Email = model.Email,
                    Pwd = model.Password
                }
            };
            var res = await _manager.LoginAsync(query);
            if (res.HasErr)
            {
                result.SetInfo(res.Msg, res.Code);
            }
            else
            {
                var user = new LoginUser();
                user.UserNo = res.Data.UserNo;
                user.UserName = res.Data.UserName;
                user.RealName = res.Data.RealName;
                user.StaffNo = res.Data.StaffNo;
                user.AdAccount = res.Data.AdAccount;
                user.Mobile = res.Data.Mobile;
                user.Email = res.Data.Email;
                user.IsAdmin = res.Data.IsAdmin;
                user.AppNo = model.AppNo;

                var claimsIdentity = _jwtFactory.GenerateClaimsIdentity(user);
                var tokenJson = await _jwtFactory.GenerateEncodedToken(user.UserNo, claimsIdentity);
                var token = JsonConvert.DeserializeObject<TokenModel>(tokenJson);

                result.SetInfo(token.auth_token, "登录成功！", 200);
            }
            result.ExpandSeconds = res.ExpandSeconds;

            return Ok(result);
        }

        /// <summary>
        /// 登录后通过token获取当前用户信息
        /// </summary>
        /// <returns></returns>
        [Authorize, HttpGet, Route("get_current_user")]
        public ActionResult GetCurrentUser()
        {
            var user = CurrentUser;

            return Ok(user);
        }
    }
}
